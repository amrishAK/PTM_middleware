using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTM_middleware.Models.API;
using PTM_middleware.BLL;
using Newtonsoft.Json;
using PTM_middleware.Models.DB;
using System.IO;

namespace PTM_middleware.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrafficController : ControllerBase
    {

        private readonly Traffic_BL _businessLogic;

        public TrafficController(Traffic_BL businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [Route("test")]
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult Echo()
        {
            return Ok(new ActionResponse(StatusCodes.Status200OK, "Controller is alive"));
        }


        [Route("openDataSource")]
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult GetOpenDataSource()
        {
            List<OpenData> availableRoutes = new List<OpenData>();
            
            using (StreamReader r = new StreamReader("roadOpenData.json"))
            {
                string jsn = r.ReadToEnd();
                availableRoutes = JsonConvert.DeserializeObject<List<OpenData>>(jsn);
            }
           
            return Ok(availableRoutes);
        }

        [Route("pedestrianLog")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetPedestrianLog()
        {
            try
            {
                var list = (List<PedestrianLog>)await _businessLogic.GetAllDocuments("PL");
                return StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                var response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
                return StatusCode(response.StatusCode, response);
            }
        }

        [Route("signalSwitchLog")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetSignalSwitchLog()
        {
            try
            {
                var list = (List<SignalSwitch>)await _businessLogic.GetAllDocuments("SS");
                return StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                var response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
                return StatusCode(response.StatusCode, response);
            }
        }

        [Route("pedestrianCrossingLog")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetPedestrianCrossingLog()
        {
            try
            {
                var list = (List<GetTemplateTD>)await _businessLogic.GetAllDocuments("PCL");
                return StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                var response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
                return StatusCode(response.StatusCode, response);
            }
        }

        [Route("trafficDensityLog")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetTrafficDensityLog()
        {
            try
            {
                var list =  (List<GetTemplateTD>) await _businessLogic.GetAllDocuments("TD");
                return StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                var response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
                return StatusCode(response.StatusCode, response);
            }
        }

        [Route("pedestrianDetail")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreatePedestrianLog([FromBody] PedestrianDetailRequest request)
        {
            ActionResponse response;

            try
            {
                bool result = await _businessLogic.CreateAsync(request);
                response = (result) ? new ActionResponse(StatusCodes.Status200OK) : new ActionResponse(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
            }

            return StatusCode(response.StatusCode, response);
        }

        [Route("signalSwitch")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateSignalSwitch([FromBody] SignalSwitchRequest request)
        {
            ActionResponse response;

            try
            {
                bool result = await _businessLogic.CreateSignalSwitchLog(request);
                response = (result) ? new ActionResponse(StatusCodes.Status200OK) : new ActionResponse(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
            }

            return StatusCode(response.StatusCode, response);
        }
    }
}
