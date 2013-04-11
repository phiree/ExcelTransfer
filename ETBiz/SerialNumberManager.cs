using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETBiz
{
    /// <summary>
    /// 流水号管理
    /// </summary>
    public class SerialNumberManager
    {
        public IDictionary<string, int> orignalSerial;

        public SerialNumberManager()
        { 
            
        }
        public int GetSerialNo(string cateAndSupplierCode)
        {
            if (!orignalSerial.ContainsKey(cateAndSupplierCode))
            {
                orignalSerial.Add(cateAndSupplierCode, 1);
            }
            else
            {
                orignalSerial[cateAndSupplierCode] = orignalSerial[cateAndSupplierCode] + 1;
            }
            return orignalSerial[cateAndSupplierCode];
        }

    }
}
