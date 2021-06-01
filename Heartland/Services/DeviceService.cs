using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heartland.Models;
using System.Text.RegularExpressions;

namespace Heartland.Services
{
    public class DeviceService
    {
        List<Devices> deviceData;
        public DeviceService()
        {
            // Adding sample data
            deviceData.Add(new Devices { serialNo = "12-1223", machineCode = "SP1029", deviceName = "Mouse" });
            deviceData.Add(new Devices { serialNo = "2939341-22112", machineCode = "MK391", deviceName = "Printer" });
            deviceData.Add(new Devices { serialNo = "11-2321", machineCode = "MC1029", deviceName = "POS" });
            deviceData.Add(new Devices { serialNo = "4-01123991", machineCode = "ACCCV3", deviceName = "Drawer" });
        }

        public IList<Devices> Search(string serialNo, string machineCode)
        {
            var list = deviceData.Where(q=>q.serialNo == serialNo || q.machineCode == machineCode).ToList();
            return list;
        }

        public void Create(Devices device)
        {
            deviceData.Add(device);
        }

        public void Update(Devices device)
        {
            deviceData = deviceData
                .Where(q=>q.serialNo==device.serialNo && q.machineCode==device.machineCode)
                .Select(w => { w.deviceName = device.deviceName; return w; }).ToList();
        }

        public ErrorMessage CheckMachineCode(string machineCode)
        {
            ErrorMessage error = null;
            if (string.IsNullOrEmpty(machineCode))
            {
                error =new ErrorMessage
                {
                    resourceKey = "machine.code.invalid",
                    errorCode = "ER001",
                    message = "The machine code is incorrect. Check the Machine code you provided and try again."
                };
            }
            return error;
        }

        public ErrorMessage CheckSerialNo(string serialNo)
        {
            ErrorMessage error = null;
            String regex1 = "^\\d{2}-\\d{4}";
            String regex2 = "^\\d{7}-\\d{5}";
            String regex3 = "^\\d{1}-\\d{8}";

            if(Regex.IsMatch(serialNo, regex1) || Regex.IsMatch(serialNo, regex2) || Regex.IsMatch(serialNo, regex3))
            {
                return null;
            }
            else
            {
                error = new ErrorMessage
                {
                    resourceKey = "serial.number.invalid",
                    errorCode = "ER003",
                    message = "The serial number entered can include a - z, A - Z, 0 - 9 and hyphen.Please correct your entry."
                };
            }
            return error;
        }
    }
}
