using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
namespace ETBiz
{
    /// <summary>
    /// 将excel文件导入 datatable,再通过sql来转换
    /// </summary>
    public class TransferInDatatable
    {

        DataSet ds = new DataSet();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xslPathBaojia">报价单</param>
        /// <param name="xslPathBianma">编码表</param>
        /// <param name="xslPathSupplier">供应商列表</param>
        /// <param name="xslPathErp">标准erp物料表</param>
        public DataTable JoinXslToDataTable(string xslPathBaojia, string xslPathBianma, string xslPathSupplier, string xslPathErp)
        {
            DataTable dtBaoJia = CreateFromXsl(xslPathBaojia);
            Console.WriteLine("报价单行数:" + dtBaoJia.Rows.Count);
            DataTable dtBianma = CreateFromXsl(xslPathBianma);
            Console.WriteLine("编码表行数:" + dtBianma.Rows.Count);

            DataTable dtErp = CreateFromXsl(xslPathErp);

            DataTable dtSupplier = CreateFromXsl(xslPathSupplier);
            Console.WriteLine("供应商数量:" + dtSupplier.Rows.Count);

            DataSet ds = new DataSet();
            ds.Tables.Add(dtBaoJia);

            // var noSupplier=from ttBaojia in dtBaoJia.AsEnumerable()


            //var result = from ttBaojiao in dtBaoJia
            //             from ttBianma in dtBianma
            //             where ttBaojiao["产品型号"] == ttBianma["产品型号"]
            //  select ttBaojiao;

            //  string productType=((string) ttBaojia["产品型号"]).Trim();
            //
            // var resultsNoSuppier= from ttBaojia in dtBaoJia.AsEnumerable()
            var results = from ttBaojia in dtBaoJia.AsEnumerable()
                          join ttBianma in dtBianma.AsEnumerable()

                        //   where 

                         // join ttSupplier in dtSupplier.AsEnumerable()
                          on

                            ttBaojia.Field<string>("总编码") equals
                               ttBianma.Field<string>("总编码")
                          //where
                          //   !string.IsNullOrEmpty( ttBaojia.Field<string>("产品型号").Trim())&&ttBaojia.Field<string>("分类编码") == (ttBianma.Field<string>("产品大类编码") +"."+ ttBianma.Field<string>("产品小类编码"))

                          join ttSupplier in dtSupplier.AsEnumerable()
              on
               ttBianma.Field<string>("供应商编码")
               equals
                   ttSupplier.Field<string>("供应商编码")

                          select new
                           {
                               代码 = ttBianma.Field<string>("总编码"),
                               名称 = ttBaojia.Field<string>("产品名称"),
                               明细 = "TRUE",
                               物料型号 = ttBianma.Field<string>("产品型号"),
                               基本计量单位_FName = ttBaojia.Field<string>("单位"),
                               规格型号 = ttBaojia.Field<string>("规格/参数"),
                               备注 = ttBaojia.Field<string>("产地"),
                               含税出厂价 = ttBaojia.Field<string>("出厂价"),
                               税率 = ttBaojia.Field<string>("税率"),
                               最小订货量 = ttBaojia.Field<string>("最小起订量"),
                               固定提前期 = ttBaojia.Field<string>("生产周期"),
                               产品描述 = "需要确定来自哪一列",
                               来源_FName = ttSupplier.Field<string>("供应商名称"),
                               来源_FNumber = ttSupplier.Field<string>("供应商编码")
                           };

            foreach (var r in results)
            {
                DataRow newRow = dtErp.NewRow();
                newRow["代码"] = r.代码;
                newRow["备注"] = r.备注 + r.产品描述;
                newRow["固定提前期"] = r.固定提前期;
                newRow["规格型号"] = r.规格型号;
                newRow["含税出厂价"] = r.含税出厂价;
                newRow["基本计量单位_FName"] = r.基本计量单位_FName;
                newRow["来源_FName"] = r.来源_FName;
                newRow["来源_FNumber"] = r.来源_FNumber;
                newRow["名称"] = r.名称;
                newRow["明细"] = r.明细;
                newRow["税率(%)"] = r.税率;
                newRow["物料型号"] = r.物料型号;
                newRow["最小订货量"] = r.最小订货量;
                dtErp.Rows.Add(newRow);
            }
            return dtErp;

        }
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

        public void CreateXslFromDataTable(DataTable dt, string xslPath, string outXslPath)
        {
            FileStream fs = new FileStream(xslPath, FileMode.Open);
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
            FileStream file = new FileStream(outXslPath, FileMode.Create);
            book.Write(file);
            file.Close();
        }


        /// <summary>
        /// 生成nts编码
        /// </summary>
        /// <param name="catelogCode"></param>
        private void BuildNtsCode(string catelogCode,string suppierCode)
        { 
            //获取当前分类和当前供应商的最大编码
            int maxNumber = 1;
            //新编码
            int newNumber = maxNumber + 1;
            string newSerialNumber = "0000" + newNumber;
            newSerialNumber = newSerialNumber.Substring(newSerialNumber.Length - 5, 5);
            string ntsCode = catelogCode + newSerialNumber;

        }
    }
}
