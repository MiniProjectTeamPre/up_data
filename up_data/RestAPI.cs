using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace up_data {
    public class RestAPI {
        #region ===================================== Private ==========================================
        private AutoItX3Lib.AutoItX3 autoit = new AutoItX3Lib.AutoItX3();
        private string url { get; set; }
        private string employeeID { get; set; }
        private string sn { get; set; }
        private string process { get; set; }
        private string workOrder { get; set; }
        private string stationName { get; set; }
        private string computerName { get; set; }
        private string partNo { get; set; }
        private string customerSN { get; set; }
        private string testStatus { get; set; }
        private bool retestFlag { get; set; }
        private bool autoitFlag { get; set; }
        private string urlList { get; set; }
        private string json { get; set; }
        private HttpWebRequest httpSend { get; set; }
        private HttpWebResponse httpRead { get; set; }
        private WaitChrome waitChrome { get; set; }
        #endregion

        #region ===================================== Public ==========================================
        public HeadConfig headConfig { get; set; }
        public MesSage mesSage { get; set; }

        public string testResult { get; set; }
        public string URL { get; set; }
        public string eMail { get; set; }
        public string passWord { get; set; }
        public bool killChrome { get; set; }
        public int killChromeDelay { get; set; }
        public bool skipREST { get; set; }
        public string project { get; set; }
        #endregion

        #region ===================================== Function Public ==========================================
        public RestAPI() {
            headConfig = new HeadConfig();
            mesSage = new MesSage();
            clear_paramitor();
        }
        public string getFinalSN(string sn_) {
            sn = sn_;
            urlList = CMD_REST.GetFinalSN;
            return send();
        }
        public string getEmployeeID(string employeeID_) {
            employeeID = employeeID_;
            urlList = CMD_REST.GetEmployeeID;
            return send();
        }
        public string addTestResult(string sn_, string employeeID_, string workOrder_, string fg_, string testStatus_,
                                    string process_, string stationName_, string computerName_) {
            sn = sn_;
            employeeID = employeeID_;
            workOrder = workOrder_;
            partNo = fg_;
            testStatus = testStatus_;
            process = process_;
            stationName = stationName_;
            computerName = computerName_;
            urlList = CMD_REST.AddTestResult;
            return send();
        }
        public string getPreviouseStep(string sn_, string process_) {
            sn = sn_;
            process = process_;
            return send();
        }
        #endregion

        #region ===================================== Function Private ==========================================
        private void clear_paramitor() {
            employeeID = "";
            sn = "";
            process = "";
            workOrder = "";
            stationName = "";
            computerName = "";
            partNo = "";
            customerSN = "";
            testResult = "";
            testStatus = "";

            json = "";
        }
        private void clear_flag() {
            retestFlag = false;
            autoitFlag = false;
        }
        private string gen_json_rest() {
            return new JavaScriptSerializer().Serialize(new {
                EmployeeID = employeeID,
                SN = sn,
                Process = process,
                WorkOrder = workOrder,
                StationName = stationName,
                ComputerName = computerName,
                PartNo = partNo,
                CustomerSN = customerSN,
                TestResult = testResult,
                TestStatus = testStatus
            });
        }
        private void send_rest(string jsonRest)
        {
            httpSend = (HttpWebRequest)WebRequest.Create(url);
            httpSend.Method = "POST";
            httpSend.ContentType = "application/json";
            try {
                StreamWriter swJSONPayload = new StreamWriter(httpSend.GetRequestStream());
                swJSONPayload.Write(jsonRest);
                swJSONPayload.Close();
            } catch { }
        }
        private void read_rest() {
            httpRead = null;
            try {
                httpRead = (HttpWebResponse)httpSend.GetResponse();
                Stream stream = httpRead.GetResponseStream();
                if (stream != null) {
                    StreamReader reader = new StreamReader(stream);
                    json = reader.ReadToEnd();
                }
            } catch (Exception ex) {
                json = "{\"errorMessages\":\"" + ex.Message.ToString() + "\"}";
            }
        }
        private string get_result_rest() {
            List<string> values = new List<string>();
            List<string> keys = new List<string>();
            string pattern = @"\""(?<key>[^\""]+)\""\:\""?(?<value>[^\"",}]+)\""?\,?";
            foreach (Match m in Regex.Matches(json, pattern)) {
                if (m.Success) {
                    values.Add(m.Groups["value"].Value);
                    keys.Add(m.Groups["key"].Value);
                }
            }

            return string.Join(",", values);
        }
        private void start_chrome() {
            //Process.Start("chrome.exe", "www.google.co.th");
            Process process_chrome = new Process();
            process_chrome.StartInfo.FileName = "chrome.exe";
            process_chrome.StartInfo.Arguments = "www.google.co.th";
            process_chrome.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process_chrome.Start();
        }
        private void wait_chrome_show() {
            waitChrome = new WaitChrome();
            waitChrome.Show();
        }
        private void wait_chrome_close() {
            waitChrome.close = true;
            waitChrome.Close();
            waitChrome.Dispose();
        }
        private void key_mail_in_chrome() {
            autoitFlag = true;
            Thread.Sleep(3000);
            autoit.Send(eMail);
            Thread.Sleep(300);
            autoit.Send("{TAB}");
            Thread.Sleep(300);
            autoit.Send("24339@p0" + passWord);
            Thread.Sleep(300);
            autoit.Send("{ENTER}");
            Thread.Sleep(5000);

            kill_chrome();
        }
        private void kill_chrome() {
            if (killChrome) {
                Process[] chrome = Process.GetProcessesByName("chrome");
                for (int kkl = 0; kkl < chrome.Count(); kkl++) {
                    Process p = chrome[kkl];
                    p.Kill();
                }
            }
        }
        #endregion

        //หลักๆ มันจะอยู่ในฟังก์ชั่นนี้ั
        private string send() {
            clear_flag();

            Resend:
            url = URL + urlList;

            string jsonRest = gen_json_rest();

            send_rest(jsonRest);

            read_rest();

            string resultRest = get_result_rest();

            if (resultRest.Contains("error: (407)")) {
                start_chrome();

                wait_chrome_show();
                Thread.Sleep(killChromeDelay);
                wait_chrome_close();

                if (retestFlag && !autoitFlag) {
                    key_mail_in_chrome();

                    goto Resend;
                }

                kill_chrome();

                if (!retestFlag) {
                    retestFlag = true;
                    Thread.Sleep(3000);
                    goto Resend;
                }
            }

            clear_paramitor();
            return resultRest;
        }

        #region ===================================== Class ==========================================
        public class CMD_REST {
            public static string GetEmployeeID = "GetEmployeeID";
            public static string GetTeamSN = "GetTeamSN";
            public static string GetFinalSN = "GetFinalSN";
            public static string GetNextStep = "GetNextStep";
            public static string GetPreviouseStep = "GetPreviouseStep";
            public static string GetFGMemeber = "GetFGMemeber";
            public static string GetWorkOrder = "GetWorkOrder";
            public static string AddAssambly = "AddAssambly";
            public static string AddMatchingCustomerSN = "AddMatchingCustomerSN";
            public static string AddTestResult = "AddTestResult";
        }
        public class HeadConfig {
            public string kill_chrome { get; set; }
            public string kill_chrome_delay { get; set; }
            public string select_url { get; set; }
            public string url_of_iot { get; set; }
            public string url_of_ip { get; set; }
            public string skip_login_via_rest { get; set; }
            public string project { get; set; }
            public string e_mail { get; set; }
            public string password { get; set; }

            public HeadConfig() {
                kill_chrome = "Kill Chrome";
                kill_chrome_delay = "Kill Chrome Delay [ms]";
                select_url = "Select URL";
                url_of_iot = "URL of Iot";
                url_of_ip = "URL of IP";
                skip_login_via_rest = "Skip Login via REST";
                project = "Project";
                password = "Password";
                e_mail = "E-mail";
            }
        }
        public class MesSage {
            public string kill_chrome_delay { get; set; }
            public string kill_chrome { get; set; }
            public string skip_rest_login { get; set; }
            public string pass { get; set; }
            public string fail { get; set; }


            public MesSage() {
                kill_chrome_delay = "แปลงค่า kill chrome delay ให้เป็น int ไม่ได้ กรุณาตรวจสอบไฟล์ config";
                kill_chrome = "แปลงค่า kill chrome ให้เป็น Boolean ไม่ได้ กรุณาตรวจสอบไฟล์ config";
                skip_rest_login = "แปลงค่า skip rest login ให้เป็น Boolean ไม่ได้ กรุณาตรวจสอบไฟล์ config";
                pass = "ผ่านขั้นตอน";
                fail = "สถานะ : FAIL";
            }
        }
        #endregion
    }
}
