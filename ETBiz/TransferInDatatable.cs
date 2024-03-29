﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Globalization;
using System.Text.RegularExpressions;
namespace ETBiz
{
    /// <summary>
    /// 将excel文件导入 datatable,再通过sql来转换
    /// </summary>
    public class TransferInDatatable
    {
        SerialNumberManager serialNumberManager = new SerialNumberManager();
         
        DataSet ds = new DataSet();

        public void Transfer(string xslBaojiandan, bool needSaveSerialNo)
        {
            DataTable dt = JoinXslToDataTable(xslBaojiandan,!needSaveSerialNo);
            CreateXslFromDataTable(dt, needSaveSerialNo);
        }

        /// <summary>
        /// 根据产品报价单和供应商编码生成物料表
        /// </summary>
        /// <param name="xslPathBaojia">报价单</param>
        /// <param name="xslPathBianma">编码表</param>
        /// <param name="xslPathSupplier">供应商列表</param>
        /// <param name="xslPathErp">标准erp物料表</param>
        public DataTable JoinXslToDataTable(string xslPathBaojia,bool istest)
        {
            DataTable dtBaoJia = CreateFromXsl(xslPathBaojia);
            Console.WriteLine("报价单行数:" + dtBaoJia.Rows.Count);
        
            DataTable dtErp = CreateFromXsl(GlobalVariables.ErpXslFileTemplate);

            DataTable dtSupplier = CreateFromXsl(GlobalVariables.XslSupplierList);
            Console.WriteLine("供应商数量:" + dtSupplier.Rows.Count);

            DataSet ds = new DataSet();
            ds.Tables.Add(dtBaoJia);
            var results =  from ttBaojia in dtBaoJia.AsEnumerable()
                           join ttSupplier in dtSupplier.AsEnumerable()
                           on ttBaojia.Field<string>("供应商名称") equals ttSupplier.Field<string>("供应商名称")
                           select new
                           {
                               分类编码 = ttBaojia.Field<string>("分类编码"),
                               名称 = ttBaojia.Field<string>("产品名称"),
                               明细 = "TRUE",
                               物料型号 = ttBaojia.Field<string>("产品型号"),
                               基本计量单位_FName = ttBaojia.Field<string>("单位"),
                               规格型号 = ttBaojia.Field<string>("规格/参数"),
                               备注 = ttBaojia.Field<string>("产地"),
                               出厂价 = ttBaojia.Field<string>("出厂价"),
                               税率 = ttBaojia.Field<string>("税率"),
                               最小订货量 = ttBaojia.Field<string>("最小起订量"),
                               固定提前期 = ttBaojia.Field<string>("生产周期"),
                               产品描述 = ttBaojia.Field<string>("产品描述"),
                               来源_FName = ttSupplier.Field<string>("供应商名称"),
                               来源_FNumber = ttSupplier.Field<string>("供应商编码")
                           };

            foreach (var r in results)
            {
                DataRow newRow = dtErp.NewRow();
                newRow["代码"] = BuildNtsCode(r.分类编码,r.来源_FNumber,istest);
                newRow["备注"] = r.备注;
                newRow["描述/卖点"] = r.产品描述;
              
                newRow["规格型号"] = r.规格型号;
                Console.WriteLine(r.出厂价);

                decimal price = 0;
                if (!string.IsNullOrEmpty(r.出厂价))
                {
                    price = decimal.Parse(Regex.Replace(r.出厂价, @"[^\d.]", ""));
                }
                int 生产周期 = 0;

                if (!string.IsNullOrEmpty(r.固定提前期))
                {
                    生产周期 = int.Parse(Regex.Replace(r.固定提前期, @"[^\d.]", ""));
                }
                int 最小订货量 = 0;

                if (!string.IsNullOrEmpty(r.最小订货量))
                {
                    最小订货量 = int.Parse(Regex.Replace(r.最小订货量, @"[^\d.]", ""));
                }
                newRow["固定提前期"] = 生产周期;
                newRow["采购单价"] = price;// decimal.Parse(r.含税出厂价, NumberStyles.AllowCurrencySymbol | NumberStyles.Number); //r.含税出厂价;
                newRow["基本计量单位_FName"] = r.基本计量单位_FName;
                newRow["来源_FName"] = r.来源_FName;
                newRow["来源_FNumber"] = r.来源_FNumber;
                newRow["名称"] = r.名称;
                newRow["明细"] = r.明细;
                newRow["税率(%)"] = r.税率;
                newRow["物料型号"] = r.物料型号;
                newRow["最小订货量"] = 最小订货量;
                newRow["最大订货量"] = 99999;
                newRow["物料属性_FName"] = "外购";
                newRow["物料分类_FName"] = "主推商品";
                newRow["计量单位组_FName"] = "数量组";

                newRow["基本计量单位_FGroupName"] = "数量组";
                newRow["基本计量单位_FName"] = "个";

                newRow["采购计量单位_FName"] = "个";
                newRow["采购计量单位_FGroupName"] = "数量组";
                newRow["销售计量单位_FName"] = "个";
                newRow["销售计量单位_FGroupName"] = "数量组";
                newRow["生产计量单位_FName"] = "个";
                newRow["生产计量单位_FGroupName"] = "数量组";
                newRow["库存计量单位_FName"] = "个";
                newRow["库存计量单位_FGroupName"] = "数量组";

                newRow["辅助计量单位换算率"] = "0";
                newRow["默认仓库_FName"] = "一号仓";
                newRow["默认仓库_FNumber"] = "0001";
                newRow["默认仓位_FName"] = "*";
                newRow["默认仓位_FGroupName"] = "*";

                newRow["数量精度"] = "4";
                newRow["最低存量"] = "1";
                newRow["最高存量"] = "11000";
                newRow["安全库存数量"] = "2";
                newRow["使用状态_FName"] = "使用";
                newRow["是否为设备"] = "False";

                newRow["存货科目代码_FNumber"] = "1001";
                newRow["销售收入科目代码_FNumber"] = "1001";
                newRow["销售成本科目代码_FNumber"] = "1001";
                newRow["计划模式_FName"] = "MTS计划模式";
                newRow["计价方法_FName"] = "加权平均法";
                newRow["变动提前期批量"] = "1";
                newRow["看板容量"] = "1";
                newRow["单价精度"] = "2";
                newRow["变动提前期"] = "0";
                newRow["标准加工批量"] = "1";
               
                dtErp.Rows.Add(newRow);
            }
            return dtErp;

        }
       /// <summary>
       /// 为每个xsl生成数据表
       /// </summary>
       /// <param name="xslPath"></param>
       /// <returns></returns>
        public DataTable CreateFromXsl(string xslPath)
        {
            return CreateFromXsl(xslPath, false);
        }
        public DataTable CreateFromXsl(string xslPath, bool onlyCreateSchedule)
        {
            FileStream fs = new FileStream(xslPath, FileMode.Open);
            HSSFWorkbook book = new HSSFWorkbook(fs);
            fs.Close();
            var sheet = book.GetSheetAt(0);
            DataTable dt = new DataTable();
            var row = sheet.GetRow(0);
            foreach (var cell in row.Cells)
            {
                DataColumn col = new DataColumn(cell.ToString(), typeof(String));
                dt.Columns.Add(col);
            }
            if (!onlyCreateSchedule)
            {
                IEnumerator rowEnumer = sheet.GetRowEnumerator();
                while (rowEnumer.MoveNext())
                {

                    var currentRow = (HSSFRow)rowEnumer.Current;
                    if (currentRow.RowNum == 0) continue;
                    //防止其遍历到没有数据的row
                    if (currentRow.LastCellNum < row.Cells.Count)
                    {
                        //    break;
                    }
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        var cell = currentRow.GetCell(i);
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        

        public void CreateXslFromDataTable(DataTable dt)
        {
            CreateXslFromDataTable(dt,false);
        }
        /// <summary>
        /// 将结果导出成erp标准xsl表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="xslPath"></param>
        /// <param name="outXslPath"></param>
        /// <param name="saveNtsNumber">将最大的nts编码保存到物理文件</param>
        public void CreateXslFromDataTable(DataTable dt,bool saveNtsNumber)
        {
            FileStream fs = new FileStream(GlobalVariables.ErpXslFileTemplate, FileMode.Open);
            HSSFWorkbook book = new HSSFWorkbook(fs);
            fs.Close();
            var sheet = book.GetSheetAt(0);
            var firstRow = sheet.GetRow(0);
            var columnNameOfXsl = firstRow.Cells;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];
                var xslRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < row.ItemArray.Length; j++)
                {
                    foreach (var cell in columnNameOfXsl)
                    {
                        if (cell.ToString() == dt.Columns[j].ColumnName)
                        {
                            xslRow.CreateCell(cell.ColumnIndex).SetCellValue(row.ItemArray[j].ToString());
                            break;
                        }
                    }
                    // 
                }
            
            }
            string outXlsFilePath = GlobalVariables.ErpXslFileOutTest;
            if (saveNtsNumber)
            {
                outXlsFilePath = GlobalVariables.ErpXslFileOut;
            }

            IOHelper.EnsureFileDirectory(outXlsFilePath);
            FileStream file = new FileStream(outXlsFilePath, FileMode.Create);

            book.Write(file);
            file.Close();
            //保存每个分类的最后编码
            if (saveNtsNumber)
            {
                serialNumberManager.WriteSerialNumberFile();
            }
        }


        /// <summary>
        /// 生成nts编码
        /// </summary>
        /// <param name="catelogCode"></param>
        public string BuildNtsCode(string catelogCode,string suppierCode,bool isTest)
        {
            string baseNumber=catelogCode+"."+suppierCode;
            //获取当前分类和当前供应商的最大编码
            int serialNumber = serialNumberManager.GetSerialNo(baseNumber,isTest);
            //新编码
            string newSerialNumber = "0000" + serialNumber;
            newSerialNumber = newSerialNumber.Substring(newSerialNumber.Length - 5, 5);
            string ntsCode = baseNumber+ newSerialNumber;
            return ntsCode;
        }
        
    }
}
