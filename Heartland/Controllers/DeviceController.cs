using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Heartland.Models;
using Heartland.Services;
using System.Net;

namespace Heartland.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        DeviceService deviceService = new DeviceService();

        // GET: Device/machineCode
        [HttpGet]
        [Route("{nachineCode:string}", Name = "SearchByMachineCode")]
        public IActionResult SearchByMachineCode(string machineCode)
        {
            // Check machineCode
            ErrorMessage err = deviceService.CheckMachineCode(machineCode);
            if(err != null)
            {
                return new JsonResult(err);
            }

            //Retrieve device
            var item = deviceService.Search(machineCode,null);
            if (item == null)
            {
                return new JsonResult(new ErrorMessage { 
                    errorCode= "ER002", 
                    message= "The machine code does not match our records.", 
                    resourceKey= "machine.code.not.found"
                });
            }
            return new JsonResult(item);
        }

        // GET: Device/machineCode
        [HttpGet]
        [Route("{nachineCode:string}", Name = "SearchByMachineCode")]
        public IActionResult SearchBySerialNo(string serialNo)
        {
            // Check machineCode
            ErrorMessage err = deviceService.CheckSerialNo(serialNo);
            if (err != null)
            {
                return new JsonResult(err);
            }

            //Retrieve device
            var item = deviceService.Search(null, serialNo);
            if (item == null)
            {
                return new JsonResult(new ErrorMessage
                {
                    errorCode = "ER004",
                    message = "The serial number does not match our records.",
                    resourceKey = "serial.number.not.found"
                });
            }
            return new JsonResult(item);
        }

        // POST: Device
        [HttpPost]
        public IActionResult Create(Devices device)
        {
            List<ErrorMessage> errors = new List<ErrorMessage>();
            ErrorMessage error =deviceService.CheckMachineCode(device.machineCode);
            if (error != null)
                errors.Add(error);
            error = deviceService.CheckSerialNo(device.serialNo);
            if (error != null)
                errors.Add(error);
            deviceService.Create(device);
            return Ok();
        }

        // POST: Device
        [HttpPost]
        public IActionResult Update(Devices device)
        {
            List<ErrorMessage> errors = new List<ErrorMessage>();
            ErrorMessage error = deviceService.CheckMachineCode(device.machineCode);
            if (error != null)
                errors.Add(error);
            error = deviceService.CheckSerialNo(device.serialNo);
            if (error != null)
                errors.Add(error);
            deviceService.Update(device);
            return Ok();
        }
    }
}
