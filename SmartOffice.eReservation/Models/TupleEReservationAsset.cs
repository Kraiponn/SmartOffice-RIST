﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.eReservation.ModelsDocControl;


namespace SmartOffice.eReservation.Models
{

    public class TupleEReservationAsset
    {
        public List<ReservationMasterAsset> reservationMasterAssetsall { get; set; }
        public List<ReservationMasterAssetImage> ReservationMasterAssetImagesall { get; set; }

        public SelectList ddlLocationList { get; set; }
    }

    
}


