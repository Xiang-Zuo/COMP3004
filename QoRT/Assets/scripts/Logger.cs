using System;
using System.IO;
using UnityEngine;

namespace QuestGame {
	public class Logger {

		//This constructor will call the init function
		//Should only be called once in your code
		public Logger() {
			this.init();
		}

		//Can be called as many time as you want in your code (as it will still construct the logger but won't call the init function
		public Logger(bool b) {} //This constructor won't call the init function

		public void logCustom(string n, string type) {
			printToFile(generateTimestamp() + " [" + type.ToUpper() + "]: " + n + "\n");
		}

		public void info(string n) {
			printToFile(generateTimestamp() + " [INFO]: " + n + "\n");
		}

		public void debug(string n) {
			printToFile(generateTimestamp() + " [DEBUG]: " + n + "\n");
		}

		public void warn(string n) {
			printToFile(generateTimestamp() + " [WARN]: " + n + "\n");
		}

		public void error(string n) {
			printToFile(generateTimestamp() + " [ERROR]: " + n + "\n");
		}

		public void trace(string n) {
			printToFile(generateTimestamp() + " [TRACE]: " + n + "\n");
		}

		public void test(string n) {
			printToFile(generateTimestamp() + " [TEST]: " + n + "\n");
		}
	
		private void init() {
            File.WriteAllText("C:/Users/XIANG ZUO/Desktop/3004/QoRT/Logs/gameLog.txt", String.Empty);
			printToFile("-------------------- INITIALIZE LOGGER ---------------------\n");
            printToFile("reference: from Lachlan (culearn)\n");
            printToFile(generateTimestamp() + ": Logger initialized\n");
		}

		private void printToFile(string n) {
			System.IO.File.AppendAllText("C:/Users/XIANG ZUO/Desktop/3004/QoRT/Logs/gameLog.txt", n);
		}

		private string generateTimestamp() {
			return DateTime.Now.ToString("O");
		}
	}
}