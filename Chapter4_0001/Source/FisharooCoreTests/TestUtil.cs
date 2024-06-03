using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Hosting;
using StructureMap;

namespace Fisharoo.FisharooCoreTests
{
	public static class TestUtil
	{
		private static bool _initialized = false;

        public static void SetUpHttpContext()
        {
            if (HttpContext.Current == null)
            {
                string path;
                path = System.IO.Path.GetDirectoryName(
                   System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                path = path.Replace("FisharooCoreTests\\bin\\Debug", "");

                TextWriter tw = new StringWriter();
                HttpWorkerRequest wr = new SimpleWorkerRequest("/FisharooWeb", path, "default.aspx", "", tw);
                HttpContext.Current = new HttpContext(wr);
            }
        }

        public static void ClearHttpContext()
        {
            HttpContext.Current = null;
        }

		public static void DeleteAndCreateCommonDevelopmentData()
		{

		}

		public static string GenerateString(char character, int length)
		{
			return string.Join(character.ToString(), new string[length + 1]);
		}
	}
}