﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartOffice.eManagement.Models;
using SmartOffice.EManagement.IResponsitory;
using SmartOffice.Models;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Class
{

    public interface IMenuMasterService2
    {
        IEnumerable<AspNetMenu> GetMenuMasterpage();
        IEnumerable<AspNetMenu> GetMenuMaster();
        IEnumerable<AspNetMenu> GetMenuMaster(String UserRole);
    }
    public class MenuMasterService2 : Controller
    {
        private readonly DocumentControlContext _dbDocumentControlContext;
        private readonly ESmartOfficeContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEUser _IEUser;
        private readonly IEForm _IEForm;

        public MenuMasterService2(ESmartOfficeContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DocumentControlContext dbDocumentControlContext, IEUser eUser,IEForm eForm)
        {
            _dbDocumentControlContext = dbDocumentControlContext;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _IEUser = eUser;
            _IEForm = eForm;
        }

        
        public TupleUser GetGroup1(string OpeGroupCateg, string Division ,string Section,string Department,string Department2,string Department3,string username,string SpecialGroup)
        {
            
            var model = _IEUser.GetGroup(OpeGroupCateg, Division, Section, Department, Department2, Department3, username, SpecialGroup);

            return model;
        }

        public TupleUser GetGroup2(string strObj, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName)
        {

            var model = _IEUser.GetGenerate(strObj, "Generate", OperationCode, OpeGroupCateg, InputKind, OperationNo ?? "", UserName);

            return model;
        }

        public TupleForm GetGroup3(string OpeGroupCateg, string Division, string Section, string Department, string Department2, string Department3, string username, string SpecialGroup)
        {

            var model = _IEForm.GetGroup(OpeGroupCateg, Division, Section, Department, Department2, Department3, username, SpecialGroup);

            return model;
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////


        public IEnumerable<menu> GetMenuMasterpage()
        {
            return null;

        }
        public async Task<AspNetGroup> GetAspNetGroup(string GroupCateg)
        {
            var groupname = await _dbContext.AspNetGroup.Where(i => i.GroupCateg == GroupCateg).FirstOrDefaultAsync();
            //await Task.WhenAll(groupname.ToListAsync());
            //await groupname.ToListAsync();

            return groupname;
        }
        public async Task<MasterMenuMaster> GetMenuMaster(string userid, string GroupCateg, int partid)
        {


            var RoleNames = await (from aspNetUsers in _dbContext.AspNetUsers
                             join aspNetUserRoles in _dbContext.AspNetUserRoles on aspNetUsers.Id equals aspNetUserRoles.UserId

                             join aspNetRoles in _dbContext.AspNetRoles on aspNetUserRoles.RoleId equals aspNetRoles.Id

                             join aspNetMenuRoles in _dbContext.AspNetMenuRoles on aspNetRoles.Id equals aspNetMenuRoles.RoleId

                             join aspNetMenu in _dbContext.AspNetMenu on aspNetMenuRoles.MenuIdentity equals aspNetMenu.MenuIdentity

                             join aspNetMenuControl in _dbContext.AspNetMenuControl on aspNetMenu.MenuIdentity equals aspNetMenuControl.MenuIdentity

                             join aspNetGroupMenu in _dbContext.AspNetGroupMenu
                             on new { a = aspNetMenuControl.GroupMenuId } equals new { a = aspNetGroupMenu.GroupMenuId }

                             join controlPart in _dbContext.ControlPart on aspNetGroupMenu.PartId equals controlPart.PartId

                             where aspNetRoles.Disable == true && aspNetMenu.Disable == true && aspNetGroupMenu.Disable == true && controlPart.Disable == true
                               && aspNetUsers.Id == userid
                               && aspNetGroupMenu.GroupCateg == GroupCateg && controlPart.PartId == partid

                             select new AspNetMenuSetup
                             {
                                 MenuIdentity = aspNetMenu.MenuIdentity,
                                 MenuName = aspNetMenu.MenuNameT + " " + aspNetMenu.MenuNameE + " " + aspNetMenu.MenuNameJ ??
                                aspNetMenu.MenuNameT + " " + aspNetMenu.MenuNameE ??
                                aspNetMenu.MenuNameT + " " + aspNetMenu.MenuNameJ ??
                                aspNetMenu.MenuNameE + " " + aspNetMenu.MenuNameJ ??
                                aspNetMenu.MenuNameT ?? aspNetMenu.MenuNameE ?? aspNetMenu.MenuNameJ,
                                 Action = aspNetMenu.Action,
                                 Controller = aspNetMenu.Controller,
                                 Param = aspNetMenu.Param,
                                 MenuUrl = aspNetMenu.MenuUrl,
                                 IconClass = aspNetMenu.IconClass,
                                 Badges = aspNetMenu.Badges,
                                 BadgesName = aspNetMenu.BadgesName,
                                 Download = aspNetMenu.Download,


                                 DisplayOrder = aspNetMenuControl.DisplayOrder,
                                 MenuIdentityParent = aspNetMenuControl.MenuIdentityParent,

                                 PartId = aspNetGroupMenu.PartId,
                                 GroupCateg = aspNetGroupMenu.GroupCateg,
                                 GroupMenuId = aspNetGroupMenu.GroupMenuId,
                                 GroupMenuName = aspNetGroupMenu.GroupMenuName,
                                 GroupDisplayOrder = aspNetGroupMenu.GroupDisplayOrder,
                                 GroupBadges = aspNetGroupMenu.Badges,
                                 GroupBadgesName = aspNetGroupMenu.BadgesName,
                                 GroupIconClass = aspNetGroupMenu.IconClass,


                             }
                             ).Distinct().OrderBy(i=>i.DisplayOrder).ToListAsync();



            

            MasterMenuMaster MasterMenuMaster = new MasterMenuMaster()
            {
                menus = RoleNames
            };
            return MasterMenuMaster;
        }
        public async Task<MasterMmenu> GetMenuMasterpage(string id)
        {

            var q =await
            (from c in _dbDocumentControlContext.DocumentGroup
             join p in _dbDocumentControlContext.Document on c.DocumentGroupCode equals p.DocumentGroupCode into ps
             from p in ps.DefaultIfEmpty()
             select new menu()
             {
                 DocumentGroupCode = c.DocumentGroupCode,
                 DocumentGroupName = c.DocumentGroupName,
                 GroupCateg = c.GroupCateg,
                 DocumentCode = p.DocumentCode,
                 DocumentName = p.DocumentNameE
             }).Where(i => i.GroupCateg == id).OrderBy(i => i.DocumentName).ToListAsync();
             
            MasterMmenu masterMmenu = new MasterMmenu()
            {
                menus = q
            };
            return masterMmenu;
        }
    }
}
