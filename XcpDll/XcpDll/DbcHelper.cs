using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisAgreeMent.Model.DBC;
using AnalysisAgreeMent.Model;
using CommonUtils.Logger;
using System.IO;

namespace AnalysisAgreeMent
{
    public class DbcHelper
    {

        private DBCData dbcData;

        public DbcHelper(DBCData dbcData)
        {
            this.dbcData = dbcData;
        }
        List<DBCMessage> dbcMessageList;
        List<DBCSignal> dbcSignalList;
        DBCMessage dbcMessage;
        DBCSignal dbcSignal;
        FileStream fs;
        StreamReader rd;

        public DbcResultEnum AnalysisDbc(string dbcPath)
        {
            if (string.IsNullOrEmpty(dbcPath))
            {
                return DbcResultEnum.FAILT;
            }
            fs = new FileStream(dbcPath, FileMode.Open);
            rd = new StreamReader(fs);
            dbcMessageList = new List<DBCMessage>();
            dbcSignalList = new List<DBCSignal>();

            dbcData.AgreeMentType = AgreementType.DBC;
            dbcData.AnalysisFileType = FileType.DBC;
            string cacheResult;
            string signString;
            while(!rd.EndOfStream)
            {
                try
                {
                    cacheResult = rd.ReadLine();
                    if (string.IsNullOrEmpty(cacheResult))
                        continue;
                    signString = cacheResult.ToUpper().TrimStart().Substring(0, 3);
                    switch (signString)
                    {
                        case DbcSearchSign.VERSION:

                            break;
                        case DbcSearchSign.NS:
                            while(true)
                            {
                                cacheResult = rd.ReadLine();
                                if (string.IsNullOrEmpty(cacheResult))
                                    continue;
                                signString = cacheResult.ToUpper().TrimStart().Substring(0,3);
                                if (signString == DbcSearchSign.BS)
                                {
                                    break;
                                }
                            }
                            break;
                        case DbcSearchSign.BS:

                            break;

                        case DbcSearchSign.BU:

                            break;
                        case DbcSearchSign.MESSAGE_START_BO:
                            //message
                            //BO_ 100 ESP_01: 8 ESP
                            AnalysisMessage(cacheResult);
                            break;
                        case DbcSearchSign.SIGNAL_SG:
                            AnalysisSignal(cacheResult,signString);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error(ex.Message);
                    return DbcResultEnum.FAILT;
                }
            }
            dbcData.DBCMessageList = dbcMessageList;
            dbcData.DBCSignalList = dbcSignalList;
            return DbcResultEnum.SUCCESS;
        }

        private void AnalysisMessage(string cacheResult)
        {
            dbcMessage = new DBCMessage();
            cacheResult = cacheResult.Substring(cacheResult.IndexOf('_') + 1).TrimStart();
            dbcMessage.FrameID = int.Parse(cacheResult.Substring(0, cacheResult.IndexOf(' ')));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf(' ') + 1).TrimStart();
            dbcMessage.FrameName = cacheResult.Substring(0, cacheResult.IndexOf(':'));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf(':') + 1).TrimStart();
            dbcMessage.MessageLen = int.Parse(cacheResult.Substring(0, cacheResult.IndexOf(' ')));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf(' ') + 1).TrimStart();
            dbcMessage.Sender = cacheResult.Substring(0).TrimEnd();
            dbcMessageList.Add(dbcMessage);
        }

        private void AnalysisSignal(string cacheResult,string signString)
        {
            dbcSignal = new DBCSignal();
            // SG_ C_ADC_IN : 23|16@0+ (1,0) [0|0] "DegreeC" Vector__XXX
            cacheResult = cacheResult.Substring(cacheResult.IndexOf('_') + 1).TrimStart();
            dbcSignal.FrameID = dbcMessage.FrameID;
            dbcSignal.SignalName = cacheResult.Substring(0, cacheResult.IndexOf(':')).TrimEnd();
            cacheResult = cacheResult.Substring(cacheResult.IndexOf(':') + 1).TrimStart();
            dbcSignal.StartBitIndex = int.Parse(cacheResult.Substring(0, cacheResult.IndexOf('|')));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf('|') + 1);
            dbcSignal.BitLength = int.Parse(cacheResult.Substring(0, cacheResult.IndexOf('@')));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf('@') + 1);
            int byteOrder = int.Parse(cacheResult.Substring(0, 1));
            if (byteOrder == 0)
                dbcSignal.ByteOrder = ByteOrder.BYTE_ORDER_MSB_LAST;
            else if (byteOrder == 1)
                dbcSignal.ByteOrder = ByteOrder.BYTE_ORDER_MSB_FIRST;
            dbcSignal.SymbolType = cacheResult.Substring(1, 1);
            cacheResult = cacheResult.Substring(cacheResult.IndexOf('(') + 1);
            dbcSignal.Factor = float.Parse(cacheResult.Substring(0, cacheResult.IndexOf(',')));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf(',') + 1);
            dbcSignal.Offset = float.Parse(cacheResult.Substring(0, cacheResult.IndexOf(')')));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf(')') + 1).TrimStart();
            dbcSignal.Minimum = float.Parse(cacheResult.Substring(cacheResult.IndexOf('[') + 1, cacheResult.IndexOf('|') - 1));
            dbcSignal.Maximun = float.Parse(cacheResult.Substring(cacheResult.IndexOf('|') + 1, cacheResult.IndexOf(']') - cacheResult.IndexOf('|') - 1));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf(']') + 1).TrimStart();

            cacheResult = cacheResult.Substring(cacheResult.IndexOf('"') + 1);
            dbcSignal.Unit = cacheResult.Substring(0, cacheResult.IndexOf('"'));
            cacheResult = cacheResult.Substring(cacheResult.IndexOf('"') + 1).TrimStart();
            dbcSignal.Receiver = cacheResult.Substring(0);
            dbcSignalList.Add(dbcSignal);
        }
    }
}
