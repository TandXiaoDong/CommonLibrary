using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FigKey.CodeGenerator;
using FigKey.CodeGenerator.Model;
using FigKey.CodeGenerator.Template;

namespace CodeGeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseConfigModel baseConfigModel = new BaseConfigModel();
            baseConfigModel.OutputEntity = "E:\\";
            //CreateCodeFile.CreateExecution(baseConfigModel, "entityCode");
            SingleTable singleTable = new SingleTable();
            var modelString = singleTable.EntityBuilder(baseConfigModel,new System.Data.DataTable());
        }
    }
}
