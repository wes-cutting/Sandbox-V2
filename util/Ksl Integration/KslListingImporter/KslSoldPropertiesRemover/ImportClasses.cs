using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

namespace KslSoldPropertiesRemover
{
	public class Classified
	{
		public int ClassifiedId { get; set; }

		[CsvColumn(FieldIndex = 0)]
		public string tid { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string cat_nid { get; set; }
		[CsvColumn(FieldIndex = 2)]
		public string nid { get; set; }
		[CsvColumn(FieldIndex = 3)]
		public string sid { get; set; }
		[CsvColumn(FieldIndex = 4)]
		public string status { get; set; }
		[CsvColumn(FieldIndex = 5)]
		public string userid { get; set; }
		[CsvColumn(FieldIndex = 6)]
		public string cmsid { get; set; }
		[CsvColumn(FieldIndex = 7)]
		public string email { get; set; }
		[CsvColumn(FieldIndex = 8)]
		public string title { get; set; }
		[CsvColumn(FieldIndex = 9)]
		public string description { get; set; }
		[CsvColumn(FieldIndex = 10)]
		public string address1 { get; set; }
		[CsvColumn(FieldIndex = 11)]
		public string address2 { get; set; }
		[CsvColumn(FieldIndex = 12)]
		public string city { get; set; }
		[CsvColumn(FieldIndex = 13)]
		public string state { get; set; }
		[CsvColumn(FieldIndex = 14)]
		public string zip { get; set; }
		[CsvColumn(FieldIndex = 15)]
		public string lat { get; set; }
		[CsvColumn(FieldIndex = 16)]
		public string lon { get; set; }
		[CsvColumn(FieldIndex = 17)]
		public string price { get; set; }
		[CsvColumn(FieldIndex = 18)]
		public string obo { get; set; }
		[CsvColumn(FieldIndex = 19)]
		public string image { get; set; }
		[CsvColumn(FieldIndex = 20)]
		public string create_time { get; set; }
		[CsvColumn(FieldIndex = 21)]
		public string expire_time { get; set; }
		[CsvColumn(FieldIndex = 22)]
		public string display_time { get; set; }
		[CsvColumn(FieldIndex = 23)]
		public string price_xtra { get; set; }
		[CsvColumn(FieldIndex = 24)]
		public string sold { get; set; }
		[CsvColumn(FieldIndex = 25)]
		public string business { get; set; }
		[CsvColumn(FieldIndex = 26)]
		public string market_type { get; set; }
		[CsvColumn(FieldIndex = 27)]
		public string stock_num { get; set; }
		[CsvColumn(FieldIndex = 28)]
		public string import_source { get; set; }
	}

	public class Home
	{
		public int HomeId { get; set; }

		[CsvColumn(FieldIndex = 0, Name = "homeid")]
		public string homekslid { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string aid { get; set; }
		[CsvColumn(FieldIndex = 2)]
		public string addr { get; set; }
		[CsvColumn(FieldIndex = 3)]
		public string city { get; set; }
		[CsvColumn(FieldIndex = 4)]
		public string state { get; set; }
		[CsvColumn(FieldIndex = 5)]
		public string zip { get; set; }
		[CsvColumn(FieldIndex = 6)]
		public string price { get; set; }
		[CsvColumn(FieldIndex = 7)]
		public string year { get; set; }
		[CsvColumn(FieldIndex = 8)]
		public string sqft { get; set; }
		[CsvColumn(FieldIndex = 9)]
		public string acres { get; set; }
		[CsvColumn(FieldIndex = 10)]
		public string bed { get; set; }
		[CsvColumn(FieldIndex = 11)]
		public string bath { get; set; }
		[CsvColumn(FieldIndex = 12)]
		public string garage { get; set; }
		[CsvColumn(FieldIndex = 13)]
		public string property { get; set; }
		[CsvColumn(FieldIndex = 14)]
		public string cooling { get; set; }
		[CsvColumn(FieldIndex = 15)]
		public string heating { get; set; }
		[CsvColumn(FieldIndex = 16)]
		public string foreclosure { get; set; }
		[CsvColumn(FieldIndex = 17)]
		public string saleby { get; set; }
		[CsvColumn(FieldIndex = 18)]
		public string member_id { get; set; }
		[CsvColumn(FieldIndex = 19)]
		public string status { get; set; }
		[CsvColumn(FieldIndex = 20)]
		public string importer { get; set; }
		[CsvColumn(FieldIndex = 21)]
		public string ctime { get; set; }
		[CsvColumn(FieldIndex = 22)]
		public string mtime { get; set; }
		[CsvColumn(FieldIndex = 23)]
		public string photo_count { get; set; }
		[CsvColumn(FieldIndex = 24)]
		public string featured { get; set; }
		[CsvColumn(FieldIndex = 25)]
		public string reservation_url { get; set; }
	}

	public class HomeCooling
	{
		[CsvColumn(FieldIndex = 0)]
		public int id { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string type { get; set; }
	}

	public class HomeHeating
	{
		[CsvColumn(FieldIndex = 0)]
		public int id { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string type { get; set; }
	}

	public class HomeProperty
	{
		[CsvColumn(FieldIndex = 0)]
		public int id { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string type { get; set; }
	}

	public class Member
	{
		[CsvColumn(FieldIndex = 0)]
		public int id { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string photo_id { get; set; }
		[CsvColumn(FieldIndex = 2)]
		public string group { get; set; }
		[CsvColumn(FieldIndex = 3)]
		public string status { get; set; }
		[CsvColumn(FieldIndex = 4)]
		public string first { get; set; }
		[CsvColumn(FieldIndex = 5)]
		public string last { get; set; }
		[CsvColumn(FieldIndex = 6)]
		public string street1 { get; set; }
		[CsvColumn(FieldIndex = 7)]
		public string street2 { get; set; }
		[CsvColumn(FieldIndex = 8)]
		public string city { get; set; }
		[CsvColumn(FieldIndex = 9)]
		public string state { get; set; }
		[CsvColumn(FieldIndex = 10)]
		public string zip { get; set; }
		[CsvColumn(FieldIndex = 11)]
		public string url { get; set; }
		[CsvColumn(FieldIndex = 12)]
		public string priPhone { get; set; }
		[CsvColumn(FieldIndex = 13)]
		public string altPhone { get; set; }
		[CsvColumn(FieldIndex = 14)]
		public string gender { get; set; }
		[CsvColumn(FieldIndex = 15)]
		public string dob { get; set; }
		[CsvColumn(FieldIndex = 16)]
		public string ssn { get; set; }
		[CsvColumn(FieldIndex = 17)]
		public string email { get; set; }
		[CsvColumn(FieldIndex = 18)]
		public string emailValid { get; set; }
		[CsvColumn(FieldIndex = 19)]
		public string emailAllow { get; set; }
		[CsvColumn(FieldIndex = 20)]
		public string emailHTML { get; set; }
		[CsvColumn(FieldIndex = 21)]
		public string emailFail { get; set; }
		[CsvColumn(FieldIndex = 22)]
		public string screenname { get; set; }
		[CsvColumn(FieldIndex = 23)]
		public string signature { get; set; }
		[CsvColumn(FieldIndex = 24)]
		public string password { get; set; }
		[CsvColumn(FieldIndex = 25)]
		public string passwordHash { get; set; }
		[CsvColumn(FieldIndex = 26)]
		public string persistent { get; set; }
		[CsvColumn(FieldIndex = 27)]
		public string loginTotal { get; set; }
		[CsvColumn(FieldIndex = 28)]
		public string loginTime { get; set; }
		[CsvColumn(FieldIndex = 29)]
		public string createTime { get; set; }
		[CsvColumn(FieldIndex = 30)]
		public string importId { get; set; }
		[CsvColumn(FieldIndex = 31)]
		public string importSrc { get; set; }
		[CsvColumn(FieldIndex = 32)]
		public string importRef { get; set; }
	}
}
