using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLabeling.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncController : ControllerBase
    {
        /// <summary>
        /// This controller synchronizez data from sensors and camera after deep learning prediction
        /// </summary>
        public SyncController()
        {

        }
    }
}
