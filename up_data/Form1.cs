using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace up_data {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            this.TransparencyKey = Color.Black;
            this.BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
        }

        #region ===================================== Define ==========================================
        private Define define = new Define();
        private SetupPay.FormPay setupPay = new SetupPay.FormPay();
        private UpPrism upPrism = new UpPrism();
        private RestAPI restAPI = new RestAPI();
        #endregion

        #region ===================================== Form Load ==========================================
        private void Form1_Load(object sender, EventArgs e) {
            setup_setupPay();
            setup_RestAPI();
            setup_prism();

            if (check_open_progarm_redundant()) return;

            write_xml_config_mis();
            check_folder_exis();
        }
        private void setup_setupPay() {
            //setupPay.form1.csv.path = "../../../Config/";
            setupPay.SelectTab = SetupPay.tabPage.TAB1;
            setupPay.set_nameTab(define.nameFileConfig);
            setupPay.SelectTab = SetupPay.tabPage.TAB2;
            setupPay.set_nameTab(define.namePrismConfig);
            setupPay.setup();
        }
        private void setup_RestAPI() {
            setupPay.SelectTab = SetupPay.tabPage.TAB1;
            try {
                restAPI.killChromeDelay = Convert.ToInt32(setupPay.read_text(restAPI.headConfig.kill_chrome_delay));
            } catch {
                MessageBox.Show(restAPI.mesSage.kill_chrome_delay);
            }

            try {
                restAPI.killChrome = Convert.ToBoolean(setupPay.read_text(restAPI.headConfig.kill_chrome));
            } catch {
                MessageBox.Show(restAPI.mesSage.kill_chrome_delay);
            }

            if (setupPay.read_text(restAPI.headConfig.select_url) == Define2.ip) {
                restAPI.URL = setupPay.read_text(restAPI.headConfig.url_of_ip);
            } else {
                restAPI.URL = setupPay.read_text(restAPI.headConfig.url_of_iot);
            }

            try {
                restAPI.skipREST = Convert.ToBoolean(setupPay.read_text(restAPI.headConfig.skip_login_via_rest));
            } catch {
                MessageBox.Show(restAPI.mesSage.skip_rest_login);
            }

            restAPI.eMail = setupPay.read_text(restAPI.headConfig.e_mail);
            restAPI.passWord = setupPay.read_text(restAPI.headConfig.password);
            restAPI.project = setupPay.read_text(restAPI.headConfig.project);
        }
        private void setup_prism() {
            setupPay.SelectTab = SetupPay.tabPage.TAB2;
            upPrism.processName = setupPay.read_text(upPrism.headConfig.processName);
            upPrism.employeeID = setupPay.read_text(upPrism.headConfig.employeeID);
            upPrism.stationName = setupPay.read_text(upPrism.headConfig.stationName);
            upPrism.computerName = setupPay.read_text(upPrism.headConfig.computerName);
            upPrism.databaseName = setupPay.read_text(upPrism.headConfig.databaseName);
            upPrism.mode = setupPay.read_text(upPrism.headConfig.mode);
        }
        private bool check_open_progarm_redundant() {
            Process[] pname = Process.GetProcessesByName(Define2.nameExe);
            if (pname.Length == 2) {
                Application.Exit();
                return true;
            }

            return false;
        }
        private void write_xml_config_mis() {
            PrismXml prismXml = new PrismXml(upPrism);
            prismXml.write();
            prismXml.Write2();

            //มันต้องเปิดปิดโปรแกรมของ mis prism ก่อนทีนึง มันถึงจะเข้าไปอ่าน config ของเขา
            TeamPrecision.PRISM.frmUserLogin mm = new TeamPrecision.PRISM.frmUserLogin();
            mm.Show();
            mm.Close();
        }
        private void check_folder_exis() {
            if (!Directory.Exists(define.path.data)) Directory.CreateDirectory(define.path.data);
            if (!Directory.Exists(define.path.data_err)) Directory.CreateDirectory(define.path.data_err);
            if (!Directory.Exists(define.path.data_passed)) Directory.CreateDirectory(define.path.data_passed);
            if (!Directory.Exists(define.path.data_success)) Directory.CreateDirectory(define.path.data_success);
        }
        #endregion

        private void timer_get_log_err_Tick(object sender, EventArgs e) {
            //return;
            timer_get_log_err.Enabled = false;
            string[] getFiles = Directory.GetFiles(define.path.data_err);
            for (int i = 0; i < getFiles.Count(); i++) {
                string s = getFiles[i].Replace(define.path.data_err + Define2.blackSlash, string.Empty);
                File.Copy(getFiles[i], define.path.data + Define2.blackSlash + s);
                File.Delete(getFiles[i]);
            }
            timer_get_log_err.Enabled = true;
        }

        #region ===================================== Fucntion ==========================================
        private List<string> fileData;
        private string nameLog;
        private string[] nameLogSplit;
        private int num_for_loop;
        public static void DelaymS(int mS)
        {
            Stopwatch stopwatchDelaymS = new Stopwatch();
            stopwatchDelaymS.Restart();
            while (mS > stopwatchDelaymS.ElapsedMilliseconds)
            {
                if (!stopwatchDelaymS.IsRunning) stopwatchDelaymS.Start();
                Application.DoEvents();
            }
            stopwatchDelaymS.Stop();
        }
        private void check_login() {
            try {
                File.ReadAllText(TXT.logIn);
                File.Delete(TXT.logIn);
                user_id userID = new user_id(restAPI);
                userID.ShowDialog();

                string fileLogin = File.ReadAllText(TXT.userID);
                File.Delete(TXT.userID);
                setupPay.SelectTab = SetupPay.tabPage.TAB2;
                setupPay.write_text(upPrism.headConfig.employeeID, fileLogin);

                if (fileLogin == string.Empty)
                {
                    File.WriteAllText(TXT.logInFail, string.Empty);
                }
                else
                    File.WriteAllText(TXT.logInOK, string.Empty);
            } catch { }
        }
        private void check_config() {
            try {
                File.ReadAllText(TXT.config);
                File.Delete(TXT.config);
                setup_setupPay();
                setup_RestAPI();
                setup_prism();
                write_xml_config_mis();
            } catch { }
        }
        private void GetOutPut() {
            try {
                upPrism.wo = File.ReadAllText(TXT.getOutPut);
                File.Delete(TXT.getOutPut);

                string result = function_timeout(upPrism.GetOutPut, 7500);
                File.WriteAllText(TXT.getOutPut_ok, result);
            } catch { }
        }
        private List<string> get_log_test() {
            List<string> fileData = new List<string>();

            try {
                string[] getFile = Directory.GetFiles(define.path.data);
                fileData = getFile.ToList<string>();
            } catch { }

            return fileData;
        }
        private string get_name_log(string fileData) {
            string nameLog = string.Empty;

            try { nameLog = fileData.Replace(define.path.data + Define2.blackSlash, string.Empty); } catch { }
            nameLog = nameLog.Replace(Define2.exclamationMark, Define2.slash);

            return nameLog;
        }
        /// <summary>
        /// Set step fail in data format json
        /// </summary>
        /// <returns>Data Failure</returns>
        private string GetStepFail() {
            JsonConvertData jsonRead;
            try {
                jsonRead = JsonConvert.DeserializeObject<JsonConvertData>(upPrism.dataSummary);
            } catch {
                return "Json";
            }
            string result = jsonRead.Failure.ToString();
            result += "," + GetDeScripTion(jsonRead);
            //result += "," + GetValueFail(jsonRead);
            return result;
        }
        /// <summary>
        /// Get description of step fail
        /// </summary>
        /// <param name="jsonRead">is object read data format json</param>
        /// <returns>description of step fail in data ormat json</returns>
        private string GetDeScripTion(JsonConvertData jsonRead) {
            int rowData = jsonRead.ResultString.Count;
            string failure = jsonRead.Failure.ToString();
            for (int loop = 0; loop < rowData; loop++) {
                if (jsonRead.ResultString[loop].Step == failure) {
                    return jsonRead.ResultString[loop].Description;
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// Get value measured of step fail
        /// </summary>
        /// <param name="jsonRead">is object read data format json</param>
        /// <returns>Value measured of step fail in data ormat json</returns>
        private string GetValueFail(JsonConvertData jsonRead) {
            int rowData = jsonRead.ResultString.Count;
            string failure = jsonRead.Failure.ToString();
            for (int loop = 0; loop < rowData; loop++) {
                if (jsonRead.ResultString[loop].Step == failure) {
                    return jsonRead.ResultString[loop].Measured;
                }
            }
            return string.Empty;
        }
        private string rest_getFinalSN() {
            string sn = restAPI.getFinalSN(nameLogSplit[1]);

            if (sn != nameLogSplit[1]) {

                if (sn.Contains(Define2.asm)) {
                    File.Move(fileData[num_for_loop], define.path.data_passed + Define2.blackSlash + nameLog);
                } else {
                    File.Move(fileData[num_for_loop], define.path.data_err + Define2.blackSlash + nameLog);
                }
                    
                return string.Empty;
            }

            return sn;
        }
        private bool rest_getPreviouseStep(string snFinal) {
            string nextStep = restAPI.getPreviouseStep(snFinal, upPrism.processName);
            // "VALID"  "!!...Proecess :FCT1 สถานะ : FAIL "    "!!...ผ่านขั้นตอน FCT1 แล้ว"

            if (nextStep != Define2.valid && !nextStep.Contains(restAPI.mesSage.fail)) {
                File.Move(fileData[num_for_loop], define.path.data_passed + Define2.blackSlash + nameLog);
                return false;
            }

            return true;
        }
        private bool rest_getResult() {
            bool result = false;

            for (int hj = 0; hj < 10; hj++) {
                //มันยังเขียน txt ไม่สมบูรณ์ แต่อีกโปรแกรมมันรีบมาอ่านก่อน

                try {
                    restAPI.testResult = File.ReadAllText(fileData[num_for_loop]);
                } catch {
                    DelaymS(100);
                    continue;
                }

                result = true;
                break;
            }

            if (!result) {
                MessageBox.Show(Define2.errLine + new StackFrame(0, true).GetFileLineNumber());
                return false;
            }

            return true;
        }
        private bool rest_addTestResult(string snFinal) {
            string result_AddTestResult = restAPI.addTestResult(snFinal, 
                                                                upPrism.employeeID, 
                                                                nameLogSplit[2], 
                                                                nameLogSplit[3], 
                                                                nameLogSplit[4],
                                                                upPrism.processName, 
                                                                upPrism.stationName, 
                                                                upPrism.computerName);


            if (result_AddTestResult != Define2.success) {
                File.Move(fileData[num_for_loop], define.path.data_err + Define2.blackSlash + nameLog);
                label1.ForeColor = Color.Red;
            } else {
                label1.ForeColor = Color.Blue;
                return false;
            }
                

            return true;
        }
        private bool rest_checkStamp(string snFinal) {
            Stopwatch timeout = new Stopwatch();
            timeout.Restart();

            while (timeout.ElapsedMilliseconds < 30000) {

                string result = restAPI.getPreviouseStep(snFinal, upPrism.processName);

                if (!result.Contains(restAPI.mesSage.pass) && !result.Contains(restAPI.mesSage.fail)) {
                    DelaymS(5000);
                    continue;
                }

                timeout.Stop();
                break;
            }


            if (timeout.IsRunning) {
                File.Move(fileData[num_for_loop], define.path.data_err + Define2.blackSlash + nameLog);
                return false;
            }


            return true;
        }
        private string rest_getSN_ASM() {
            string snASM = restAPI.getFinalSN(nameLogSplit[1]);

            if (!snASM.Contains(Define2.asm)) {
                File.Move(fileData[num_for_loop], define.path.data_err + Define2.blackSlash + nameLog);
                return string.Empty;
            }

            return snASM;
        }
        private bool dll_upPrism() {
            bool flagUpPrism = false;

            for (int hjkh = 0; hjkh < 2; hjkh++) {
                //string result = function_timeout(upPrism.up_prism, 7500);
                string result = TeamPrecision.PRISM.cResults.SaveTestResult(upPrism.sn, upPrism.result, upPrism.dataSummary);
                if (result != Define2.success)
                {
                    LogUpPrismError(upPrism.sn, "SaveTestResult", result);
                    continue;
                }
                flagUpPrism = true;
                break;
            }

            if (!flagUpPrism) {
                File.Move(fileData[num_for_loop], define.path.data_err + Define2.blackSlash + nameLog);
                LogUpPrismError(upPrism.sn, "MoveFile", String.Empty);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Up yield to prism with dll
        /// </summary>
        /// <returns>True is SUCCESS</returns>
        private bool DllUpYield() {
            for (int hjkh = 0; hjkh < 2; hjkh++) {
                string result = TeamPrecision.PRISM.cResults.saveYield(upPrism.fg, upPrism.wo.Replace("-", "/"), 
                    upPrism.processName, upPrism.sn, upPrism.result, upPrism.failure, upPrism.employeeID);
                //if (result != Define2.success) continue;
                break;
            }
            return true;
        }
        private bool dll_checkStamp() {
            Stopwatch tt = new Stopwatch();
            tt.Restart();

            while (tt.ElapsedMilliseconds < 30000) {

                //string result = function_timeout(upPrism.check_prism, 7500);
                string result = TeamPrecision.PRISM.cSNs.CheckStatusSN(upPrism.sn);

                if (!result.Contains(restAPI.mesSage.pass) && 
                    !result.Contains(restAPI.mesSage.fail)) {
                    DelaymS(500);
                    LogUpPrismError(upPrism.sn, "CheckStatusSN", result);
                    continue;
                }

                tt.Stop();
                break;
            }

            if (tt.IsRunning) {
                File.Move(fileData[num_for_loop], define.path.data_err + Define2.blackSlash + nameLog);
                LogUpPrismError(upPrism.sn, "MoveFile", String.Empty);
                return false;
            }

            return true;
        }
        private bool dll_getSN_ASM() {
            //string snASM = function_timeout(upPrism.get_snASM, 7500);
            string snASM = TeamPrecision.PRISM.cSNs.getSerialASM(upPrism.sn);

            upPrism.sn = snASM;

            if (!snASM.Contains(Define2.asm)) {
                File.Move(fileData[num_for_loop], define.path.data_err + Define2.blackSlash + nameLog);
                return false;
            }

            return true;
        }

        /// <summary>
        /// For log data error up prism
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="message"></param>
        public void LogUpPrismError(string sn, string cmd, string message) {
            string path = "D:\\LogError\\UpPrism";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DateTime now = DateTime.Now;
            StreamWriter swOut = new StreamWriter(path + "\\" + now.Year + "_" + now.Month + ".csv", true);
            string time = now.Day.ToString("00") + ":" + now.Hour.ToString("00") + ":" + now.Minute.ToString("00") + ":" + now.Second.ToString("00");
            swOut.WriteLine(time + ",SN=" + sn + ",CMD=" + cmd + ",Message=" + message);
            swOut.Close();
        }
        #endregion

        #region ===================================== Process ==========================================
        private void stamp_rest() {
            //แปลงเครื่องหมาย ลบ ให้เป็น หาร
            //เพราะชื่อไฟล์ มันเป็นเครื่องหมาย หาร ไม่ได้
            nameLogSplit[2] = nameLogSplit[2].Replace(Define2.minus, Define2.slash);

            //ตาม process ของ rest มันต้อง get final sn ก่อน
            //ถ้า get final sn ไม่ได้ แสดงว่ามีอะไรผิดสักอย่าง โปรแกรมจะเด้งออกทันที
            string snFinal = rest_getFinalSN();
            if (snFinal == string.Empty) return;

            //ตรวจสอบดูว่า sn ที่จะอัพ อยู่ที่ process ที่เราต้องการอัพหรือเปล่า
            if (!rest_getPreviouseStep(snFinal)) return;

            //get ผลการทดสอบ จาก main ที่ส่งมาเป็น txt file ไว้อยู่แล้ว
            if (!rest_getResult()) return;

            //ทำการ อัพขึ้นระบบ แล้วตรวจสอบว่า ระบบตอบกลับมาว่า SUCCESS หรือไม่
            if (!rest_addTestResult(snFinal)) return;

            //เช็คด้วยระบบอีกครั้งว่า sn ที่เราอัพขึ้นไปนั้น ได้อัพขึ้นไปแล้วจริงๆ
            if (!rest_checkStamp(snFinal)) return;

            //ย้ายไฟล์ไปเก็บไว้อีกที่นึง สำหรับ sn ที่อัพขึ้นระบบสำเร็จแล้ว
            File.Move(fileData[num_for_loop], fileData[num_for_loop].Replace(define.path.data, define.path.data_success));
            //File.Delete(file_data[num_for_loop]);
        }
        private void stamp_rest_asm() {
            //แปลงเครื่องหมาย ลบ ให้เป็น หาร
            //เพราะชื่อไฟล์ มันเป็นเครื่องหมาย หาร ไม่ได้
            nameLogSplit[2] = nameLogSplit[2].Replace(Define2.minus, Define2.slash);

            //ตาม process ของ rest มันต้อง get final sn ก่อน
            //ถ้าเป็นงานที่ assambly มาแล้ว มันจะอ่านได้่เลข asm ออกมา
            //ถ้า get final sn ไม่ได้ แสดงว่ามีอะไรผิดสักอย่าง โปรแกรมจะเด้งออกทันที
            string snASM = rest_getSN_ASM();

            //ตรวจสอบดูว่า sn ที่จะอัพ อยู่ที่ process ที่เราต้องการอัพหรือเปล่า
            if (!rest_getPreviouseStep(snASM)) return;

            //get ผลการทดสอบ จาก main ที่ส่งมาเป็น txt file ไว้อยู่แล้ว
            if (!rest_getResult()) return;

            //ทำการ อัพขึ้นระบบ แล้วตรวจสอบว่า ระบบตอบกลับมาว่า SUCCESS หรือไม่
            if (!rest_addTestResult(snASM)) return;

            //เช็คด้วยระบบอีกครั้งว่า sn ที่เราอัพขึ้นไปนั้น ได้อัพขึ้นไปแล้วจริงๆ
            if (!rest_checkStamp(snASM)) return;

            //ย้ายไฟล์ไปเก็บไว้อีกที่นึง สำหรับ sn ที่อัพขึ้นระบบสำเร็จแล้ว
            File.Move(fileData[num_for_loop], fileData[num_for_loop].Replace(define.path.data, define.path.data_success));
            //File.Delete(file_data[num_for_loop]);
        }
        private void stamp_dll_asm() {
            //get ผลการทดสอบ จาก main ที่ส่งมาเป็น txt file ไว้อยู่แล้ว
            if (!rest_getResult()) return;

            //ตามเงื่อนไขของ dll ต้องเซ็ต employee id ไปใน dll ตอนอัพมันจะไปดึงมาเอง
            upPrism.employeeID = nameLogSplit[0];
            TeamPrecision.PRISM.cSettingValues.EmployeeID = upPrism.employeeID;

            //ตัดค่าที่ต้องใช้มาใส่ class upPrism ไว้
            upPrism.sn = nameLogSplit[1];
            upPrism.wo = nameLogSplit[2];
            upPrism.fg = nameLogSplit[3];
            upPrism.result = nameLogSplit[4];
            upPrism.dataSummary = restAPI.testResult.Replace(Define2.singleQuotationMarks, string.Empty);
            upPrism.failure = GetStepFail();

            //ทำการอัพ prism ด้วย dll
            if (!dll_upPrism()) return;

            //Up yield to prism
            DllUpYield();

            //บางงาน มันต้อง เอาเลข sn ASM มาเพื่อจะเช็คว่ามันอัพขึ้นไปแล้วจริงๆ
            if (!dll_getSN_ASM()) return;

            //ตรวจสอบด้วย dll ให้แน่ใจว่าที่อัพไปนั้นอยู่บนระบบแล้ว
            if (!dll_checkStamp()) return;

            //ย้ายไฟล์ไปเก็บไว้อีกที่นึง สำหรับ sn ที่อัพขึ้นระบบสำเร็จแล้ว
            File.Move(fileData[num_for_loop], fileData[num_for_loop].Replace(define.path.data, define.path.data_success));
            //File.Delete(file_data[num_for_loop]);
        }
        private void stamp_dll() {
            //get ผลการทดสอบ จาก main ที่ส่งมาเป็น txt file ไว้อยู่แล้ว
            if (!rest_getResult()) return;

            //ตามเงื่อนไขของ dll ต้องเซ็ต employee id ไปใน dll ตอนอัพมันจะไปดึงมาเอง
            upPrism.employeeID = nameLogSplit[0];
            TeamPrecision.PRISM.cSettingValues.EmployeeID = upPrism.employeeID;

            //ตัดค่าที่ต้องใช้มาใส่ class upPrism ไว้
            upPrism.sn = nameLogSplit[1];
            upPrism.wo = nameLogSplit[2];
            upPrism.fg = nameLogSplit[3];
            upPrism.result = nameLogSplit[4];
            upPrism.dataSummary = restAPI.testResult.Replace(Define2.singleQuotationMarks, string.Empty);
            upPrism.failure = GetStepFail();

            //ทำการอัพ prism ด้วย dll
            if (!dll_upPrism()) return;

            //Up yield to prism
            DllUpYield();

            //ตรวจสอบด้วย dll ให้แน่ใจว่าที่อัพไปนั้นอยู่บนระบบแล้ว
            if (!dll_checkStamp()) return;

            //ย้ายไฟล์ไปเก็บไว้อีกที่นึง สำหรับ sn ที่อัพขึ้นระบบสำเร็จแล้ว
            File.Move(fileData[num_for_loop], fileData[num_for_loop].Replace(define.path.data, define.path.data_success));
            //File.Delete(file_data[num_for_loop]);
        }
        private void application_run_Tick(object sender, EventArgs e) {
            application_run.Enabled = false;

            //เช็คว่า main ต้องการ login รึเปล่า
            check_login();

            //เช็คว่า main ต้องการ config ค่าต่างๆ รึเปล่า
            check_config();

            //get all file ในโฟลเดอร์ ที่จะส่งขึ้นระบบ
            fileData = get_log_test();

            //เคลียตัวแปล ชื่อไฟล์
            nameLog = string.Empty;

            //เมื่อเข้าใน function run tick 1 ครั้ง ถ้าเจอไฟล์ที่จะอัพขึ้นระบบมากกว่า 10 ไฟล์ จะอัพขึ้นครั้งละ 10 ไฟล์ นอกนั้นรอรอบถัดไป
            //ป้องกัน เวลาเจอไฟล์เยอะๆครั้งเดียว โปรแกรมมันจะได้ไม่วนค้างในนี้นานๆ
            for (num_for_loop = 0; num_for_loop < 10; num_for_loop++) {

                if(fileData.Count == num_for_loop) {
                    break;
                }

                //อ่านชื่อไฟล์ที่จะอัพขึ้น อันปัจจุบัน
                nameLog = get_name_log(fileData[num_for_loop]);

                //กรณีแคลส เนื่องจากอ่านชื่อไฟล์ไม่ได้ โปรแกรมจะดีดตัวออกไป
                if (nameLog == string.Empty) break;

                //ตัดชื่อไฟล์ออกเป็น อาเรย์
                nameLogSplit = nameLog.Split('_');


                //ตรวจว่าจะโปรเจคไหน ควรจะเข้าเงื่อนไขไหน
                if (restAPI.project == define.project.denaliF1 ||
                    restAPI.project == define.project.lppF1 ||
                    restAPI.project == define.project.smrF1 ||
                    restAPI.project == define.project.sugarloafF1 ||
                    restAPI.project == define.project.sollatekF1 ||
                    restAPI.project == define.project.sollatekF2 ||
                    restAPI.project == define.project.linak10SmiF1 ||
                    restAPI.project == define.project.valencellF1) {
                    //stamp_rest();
                    stamp_dll();
                }
                if (restAPI.project == define.project.dryIceF1) {
                    //stamp_rest_asm();
                    stamp_dll_asm();
                }
                if (restAPI.project == define.project.denaliNextgenF1 ||
                    restAPI.project == define.project.denaliNextgenF2) {
                    stamp_dll_asm();
                 }
            }

            application_run.Enabled = true;
        }
        #endregion

        public string function_timeout(Func<string> function, int timeout) {
            Task<string> task = Task.Run(function);
            if (task.Wait(timeout)) return task.Result;
            else return Define2.overTimeout;
        }

        #region ===================================== Event ==========================================
        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }
        private void label1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                label1.Capture = false;
                const int WM_NCLBUTTONDOWN = 0x00A1;
                const int HTCAPTION = 2;
                Message msg = Message.Create(this.Handle, WM_NCLBUTTONDOWN, new IntPtr(HTCAPTION), IntPtr.Zero);
                this.DefWndProc(ref msg);
            }
        }
        #endregion
    }

    #region ===================================== Class ==========================================
    public class UpPrism {
        public string sn { get; set; }
        public string result { get; set; }
        public string dataSummary { get; set; }
        public string failure { get; set; }
        public string wo { get; set; }
        public string fg { get; set; }
        public HeadConfig headConfig { get; set; }
        public string stationName { get; set; }
        public string processName { get; set; }
        public string employeeID { get; set; }
        public string computerName { get; set; }
        public string databaseName { get; set; }
        public string mode { get; set; }


        public UpPrism() {
            sn = string.Empty;
            result = string.Empty;
            dataSummary = string.Empty;
            wo = string.Empty;
            headConfig = new HeadConfig();
            stationName = string.Empty;
            processName = string.Empty;
            employeeID = string.Empty;
            computerName = string.Empty;
        }
        public string up_prism() {
            return TeamPrecision.PRISM.cResults.SaveTestResult(sn, result, dataSummary);
        }
        public string check_prism() {
            return TeamPrecision.PRISM.cSNs.CheckStatusSN(sn);
        }
        public string check_prism2() {
            string[] check = TeamPrecision.PRISM.cSNs.CheckStatusSNv2(sn, wo);
            return check[1];
        }
        public string get_snASM() {
            return TeamPrecision.PRISM.cSNs.getSerialASM(sn);
        }
        public string GetOutPut() {
            string[] strArrGetWO = TeamPrecision.PRISM.cSNs.getWO(wo, TeamPrecision.PRISM.cSettingValues.ProcessName);
            return strArrGetWO[4];
        }

        public class HeadConfig {
            public string mode { get; set; }
            public string databaseName { get; set; }
            public string databaseServerTPP { get; set; }
            public string databaseServerTPR { get; set; }
            public string computerName { get; set; }
            public string stationName { get; set; }
            public string processName { get; set; }
            public string employeeID { get; set; }
            public string checkProcessBefore { get; set; }
            public string ProcessBefore { get; set; }
            public string digitSN { get; set; }


            public HeadConfig() {
                mode = "Mode";
                databaseName = "Database Name";
                databaseServerTPP = "Database Server TPP";
                databaseServerTPR = "Database Server TPR";
                computerName = "Computer Name";
                stationName = "Station Name";
                processName = "Process Name";
                employeeID = "Employee ID";
                checkProcessBefore = "Check Process Before";
                ProcessBefore = "Process Before";
                digitSN = "Digit SN";
            }
        }
    }
    public class PrismXml
    {
        public UpPrism upPrism { get; set; }
        /// <summary>Value = "TPR_PRISM"</summary>
        private string tprPrism { get; set; }
        private string databaseServer { get; set; }
        private string databaseName { get; set; }

        public PrismXml(UpPrism upPrism_)
        {
            upPrism = upPrism_;
            tprPrism = "TPR_PRISM";
        }
        /// <summary>
        /// For .xml Read file xml config and write new config
        /// </summary>
        public void write()
        {
            if (upPrism.databaseName == tprPrism)
            {
                databaseServer = "Vd147+pBWChvWVcRsdZvHQ==";
                databaseName = "awiuCMQfI7kyjwyD3O/AmA==";
            }
            else
            {
                databaseServer = "Vd147+pBWCihy3FzdahxTg==";
                databaseName = "U/AFYtHi4S8yjwyD3O/AmA==";
            }

            XmlTextWriter writer = new XmlTextWriter("TeamPrecision.PRISM.Setting.xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument();
            writer.Formatting = System.Xml.Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("cSettingSerial");
            writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            writer.WriteStartElement("TestingMode");
            writer.WriteString(upPrism.mode);
            writer.WriteEndElement();
            writer.WriteStartElement("DatabaseServer");
            writer.WriteString(databaseServer);
            writer.WriteEndElement();
            writer.WriteStartElement("DatabaseName");
            writer.WriteString(databaseName);
            writer.WriteEndElement();
            writer.WriteStartElement("DatabaseUser");
            writer.WriteString("qNMPB0293rI=");
            writer.WriteEndElement();
            writer.WriteStartElement("DatabasePassword");
            writer.WriteString("m/2+3pRMmYg=");
            writer.WriteEndElement();
            writer.WriteStartElement("ComputerName");
            writer.WriteString(upPrism.computerName);
            writer.WriteEndElement();
            writer.WriteStartElement("StationName");
            writer.WriteString(upPrism.stationName);
            writer.WriteEndElement();
            writer.WriteStartElement("ProcessName");
            writer.WriteString(upPrism.processName); 
            writer.WriteEndElement();
            writer.WriteStartElement("UsePasswordWhenLogin");
            writer.WriteString("false");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
        /// <summary>
        /// For .config Read file xml config and write new config
        /// </summary>
        public void Write2() {
            if (upPrism.databaseName == tprPrism) {
                databaseServer = "Annop Server";
                databaseName = "PRISM_TPR";
            } else {
                databaseServer = "Annop Server";
                databaseName = "PRISM_TPP";
            }

            string nameFileConfig = "TeamPrecision.PRISM.dll.config";
            XmlSerializer reader = new XmlSerializer(typeof(Configuration));
            StreamReader file = new StreamReader(nameFileConfig);
            Configuration overview = (Configuration)reader.Deserialize(file);
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[0].Value = upPrism.mode;
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[1].Value = databaseServer;
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[2].Value = databaseName;
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[5].Value = upPrism.computerName;
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[6].Value = upPrism.stationName;
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[7].Value = upPrism.processName;
            file.Close();

            var writer = new XmlSerializer(typeof(Configuration));
            var wfile = new StreamWriter(nameFileConfig);
            writer.Serialize(wfile, overview);
            wfile.Close();
        }

        [XmlRoot(ElementName = "section")]
        public class Section {

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }

            [XmlAttribute(AttributeName = "allowExeDefinition")]
            public string AllowExeDefinition { get; set; }

            [XmlAttribute(AttributeName = "requirePermission")]
            public bool RequirePermission { get; set; }
        }
        [XmlRoot(ElementName = "sectionGroup")]
        public class SectionGroup {

            [XmlElement(ElementName = "section")]
            public Section Section { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
        }
        [XmlRoot(ElementName = "configSections")]
        public class ConfigSections {

            [XmlElement(ElementName = "sectionGroup")]
            public SectionGroup SectionGroup { get; set; }
        }
        [XmlRoot(ElementName = "setting")]
        public class Setting {

            [XmlElement(ElementName = "value")]
            public string Value { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "serializeAs")]
            public string SerializeAs { get; set; }

            [XmlText]
            public string Text { get; set; }
        }
        [XmlRoot(ElementName = "TeamPrecision.PRISM.Properties.Settings")]
        public class TeamPrecisionPRISMPropertiesSettings {

            [XmlElement(ElementName = "setting")]
            public List<Setting> Setting { get; set; }
        }
        [XmlRoot(ElementName = "userSettings")]
        public class UserSettings {

            [XmlElement(ElementName = "TeamPrecision.PRISM.Properties.Settings")]
            public TeamPrecisionPRISMPropertiesSettings TeamPrecisionPRISMPropertiesSettings { get; set; }
        }
        [XmlRoot(ElementName = "supportedRuntime")]
        public class SupportedRuntime {

            [XmlAttribute(AttributeName = "version")]
            public string Version { get; set; }

            [XmlAttribute(AttributeName = "sku")]
            public string Sku { get; set; }
        }
        [XmlRoot(ElementName = "startup")]
        public class Startup {

            [XmlElement(ElementName = "supportedRuntime")]
            public SupportedRuntime SupportedRuntime { get; set; }
        }
        [XmlRoot(ElementName = "configuration")]
        public class Configuration {

            [XmlElement(ElementName = "configSections")]
            public ConfigSections ConfigSections { get; set; }

            [XmlElement(ElementName = "userSettings")]
            public UserSettings UserSettings { get; set; }

            [XmlElement(ElementName = "startup")]
            public Startup Startup { get; set; }
        }
    }
    public static class TXT {
        /// <summary>value = "up_data_login.txt"</summary>
        public static readonly string logIn = "up_data_login.txt";
        /// <summary>value = "up_data_user_id.txt"</summary>
        public static readonly string userID = "up_data_user_id.txt";
        /// <summary>value = "up_data_login_fail.txt"</summary>
        public static readonly string logInFail = "up_data_login_fail.txt";
        /// <summary>value = "up_data_login_ok.txt"</summary>
        public static readonly string logInOK = "up_data_login_ok.txt";
        /// <summary>value = "up_data_config.txt"</summary>
        public static readonly string config = "up_data_config.txt";
        /// <summary>value = "up_data_getOutPut.txt"</summary>
        public static readonly string getOutPut = "up_data_getOutPut.txt";
        /// <summary>value = "up_data_getOutPut_ok.txt"</summary>
        public static readonly string getOutPut_ok = "up_data_getOutPut_ok.txt";
    }
    public static class Define2 {
        /// <summary>Value = "IP"</summary>
        public static readonly string ip = "IP";
        /// <summary>Value = "up_data"</summary>
        public static readonly string nameExe = "up_data";
        /// <summary>Value = "\\"</summary>
        public static readonly string blackSlash = "\\";
        /// <summary>Value = "!"</summary>
        public static readonly string exclamationMark = "!";
        /// <summary>Value = "/"</summary>
        public static readonly string slash = "/";
        /// <summary>Value = "ASM"</summary>
        public static readonly string asm = "ASM";
        /// <summary>Value = "VALID"</summary>
        public static readonly string valid = "VALID";
        /// <summary>Value = "ERR Line: "</summary>
        public static readonly string errLine = "ERR Line: ";
        /// <summary>Value = "SUCCESS"</summary>
        public static readonly string success = "SUCCESS";
        /// <summary>Value = "-"</summary>
        public static readonly string minus = "-";
        /// <summary>Value = "'"</summary>
        public static readonly string singleQuotationMarks = "'";
        /// <summary>Value = "over timeout"</summary>
        public static readonly string overTimeout = "over timeout";
    }
    /// <summary>
    /// Class for read data format json
    /// </summary>
    public class JsonConvertData {
        public string Date { get; set; }
        public string Time { get; set; }
        public string LoginID { get; set; }
        public string SWVersion { get; set; }
        public string FWVersion { get; set; }
        public string SpecVersion { get; set; }
        public string TestTime { get; set; }
        public string LoadInOut { get; set; }
        public string Mode { get; set; }
        public string FinalResult { get; set; }
        public string SN { get; set; }
        public object Failure { get; set; }
        public List<ResultString_> ResultString { get; set; }

        public JsonConvertData() {
            Date = string.Empty;
            Time = string.Empty;
            LoginID = string.Empty;
            SWVersion = string.Empty;
            FWVersion = string.Empty;
            SpecVersion = string.Empty;
            TestTime = string.Empty;
            LoadInOut = string.Empty;
            Mode = string.Empty;
            FinalResult = string.Empty;
            SN = string.Empty;
            Failure = string.Empty;
            ResultString = new List<ResultString_>();
        }
        public class ResultString_ {
            public string Step { get; set; }
            public string Description { get; set; }
            public string Tolerance { get; set; }
            public string Measured { get; set; }
            public string Result { get; set; }

            public ResultString_() {
                Step = string.Empty;
                Description = string.Empty;
                Tolerance = string.Empty;
                Measured = string.Empty;
                Result = string.Empty;
            }
        }
    }
    #endregion
}
