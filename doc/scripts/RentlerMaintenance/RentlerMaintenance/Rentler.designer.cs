﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentlerMaintenance
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Rentler.Data.RentlerContext")]
	public partial class RentlerDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBuilding(Building instance);
    partial void UpdateBuilding(Building instance);
    partial void DeleteBuilding(Building instance);
    #endregion
		
		public RentlerDataContext() : 
				base(global::RentlerMaintenance.Properties.Settings.Default.Rentler_Data_RentlerContextConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public RentlerDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RentlerDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RentlerDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RentlerDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Building> Buildings
		{
			get
			{
				return this.GetTable<Building>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Buildings")]
	public partial class Building : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _BuildingId;
		
		private int _UserId;
		
		private System.Nullable<System.DateTime> _CreateDateUtc;
		
		private string _CreatedBy;
		
		private System.Nullable<System.DateTime> _UpdateDateUtc;
		
		private string _UpdatedBy;
		
		private string _RibbonId;
		
		private System.Nullable<System.DateTime> _DateRibbonActivated;
		
		private System.Nullable<int> _ContactInfoId;
		
		private string _Address1;
		
		private string _Address2;
		
		private string _City;
		
		private string _State;
		
		private string _Zip;
		
		private float _Latitude;
		
		private float _Longitude;
		
		private int _PropertyTypeCode;
		
		private string _Title;
		
		private string _Description;
		
		private System.Nullable<int> _SquareFeet;
		
		private System.Nullable<float> _Acres;
		
		private System.Nullable<int> _YearBuilt;
		
		private System.Nullable<int> _Bedrooms;
		
		private System.Nullable<float> _Bathrooms;
		
		private decimal _Price;
		
		private System.Nullable<System.DateTime> _DateAvailableUtc;
		
		private System.Nullable<System.DateTime> _DateActivatedUtc;
		
		private bool _IsActive;
		
		private bool _IsCreditCheckRequired;
		
		private bool _IsBackgroundCheckRequired;
		
		private decimal _Deposit;
		
		private decimal _RefundableDeposit;
		
		private int _LeaseLengthCode;
		
		private bool _IsSmokingAllowed;
		
		private bool _ArePetsAllowed;
		
		private bool _IsRemovedByAdmin;
		
		private bool _IsReported;
		
		private string _ReportedText;
		
		private bool _IsDeleted;
		
		private bool _HasPriority;
		
		private System.Nullable<System.DateTime> _DatePrioritized;
		
		private decimal _PetFee;
		
		private System.Nullable<int> _TemporaryOrderId;
		
		private System.Nullable<System.Guid> _PrimaryPhotoId;
		
		private string _PrimaryPhotoExtension;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnBuildingIdChanging(long value);
    partial void OnBuildingIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnCreateDateUtcChanging(System.Nullable<System.DateTime> value);
    partial void OnCreateDateUtcChanged();
    partial void OnCreatedByChanging(string value);
    partial void OnCreatedByChanged();
    partial void OnUpdateDateUtcChanging(System.Nullable<System.DateTime> value);
    partial void OnUpdateDateUtcChanged();
    partial void OnUpdatedByChanging(string value);
    partial void OnUpdatedByChanged();
    partial void OnRibbonIdChanging(string value);
    partial void OnRibbonIdChanged();
    partial void OnDateRibbonActivatedChanging(System.Nullable<System.DateTime> value);
    partial void OnDateRibbonActivatedChanged();
    partial void OnContactInfoIdChanging(System.Nullable<int> value);
    partial void OnContactInfoIdChanged();
    partial void OnAddress1Changing(string value);
    partial void OnAddress1Changed();
    partial void OnAddress2Changing(string value);
    partial void OnAddress2Changed();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnStateChanging(string value);
    partial void OnStateChanged();
    partial void OnZipChanging(string value);
    partial void OnZipChanged();
    partial void OnLatitudeChanging(float value);
    partial void OnLatitudeChanged();
    partial void OnLongitudeChanging(float value);
    partial void OnLongitudeChanged();
    partial void OnPropertyTypeCodeChanging(int value);
    partial void OnPropertyTypeCodeChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnSquareFeetChanging(System.Nullable<int> value);
    partial void OnSquareFeetChanged();
    partial void OnAcresChanging(System.Nullable<float> value);
    partial void OnAcresChanged();
    partial void OnYearBuiltChanging(System.Nullable<int> value);
    partial void OnYearBuiltChanged();
    partial void OnBedroomsChanging(System.Nullable<int> value);
    partial void OnBedroomsChanged();
    partial void OnBathroomsChanging(System.Nullable<float> value);
    partial void OnBathroomsChanged();
    partial void OnPriceChanging(decimal value);
    partial void OnPriceChanged();
    partial void OnDateAvailableUtcChanging(System.Nullable<System.DateTime> value);
    partial void OnDateAvailableUtcChanged();
    partial void OnDateActivatedUtcChanging(System.Nullable<System.DateTime> value);
    partial void OnDateActivatedUtcChanged();
    partial void OnIsActiveChanging(bool value);
    partial void OnIsActiveChanged();
    partial void OnIsCreditCheckRequiredChanging(bool value);
    partial void OnIsCreditCheckRequiredChanged();
    partial void OnIsBackgroundCheckRequiredChanging(bool value);
    partial void OnIsBackgroundCheckRequiredChanged();
    partial void OnDepositChanging(decimal value);
    partial void OnDepositChanged();
    partial void OnRefundableDepositChanging(decimal value);
    partial void OnRefundableDepositChanged();
    partial void OnLeaseLengthCodeChanging(int value);
    partial void OnLeaseLengthCodeChanged();
    partial void OnIsSmokingAllowedChanging(bool value);
    partial void OnIsSmokingAllowedChanged();
    partial void OnArePetsAllowedChanging(bool value);
    partial void OnArePetsAllowedChanged();
    partial void OnIsRemovedByAdminChanging(bool value);
    partial void OnIsRemovedByAdminChanged();
    partial void OnIsReportedChanging(bool value);
    partial void OnIsReportedChanged();
    partial void OnReportedTextChanging(string value);
    partial void OnReportedTextChanged();
    partial void OnIsDeletedChanging(bool value);
    partial void OnIsDeletedChanged();
    partial void OnHasPriorityChanging(bool value);
    partial void OnHasPriorityChanged();
    partial void OnDatePrioritizedChanging(System.Nullable<System.DateTime> value);
    partial void OnDatePrioritizedChanged();
    partial void OnPetFeeChanging(decimal value);
    partial void OnPetFeeChanged();
    partial void OnTemporaryOrderIdChanging(System.Nullable<int> value);
    partial void OnTemporaryOrderIdChanged();
    partial void OnPrimaryPhotoIdChanging(System.Nullable<System.Guid> value);
    partial void OnPrimaryPhotoIdChanged();
    partial void OnPrimaryPhotoExtensionChanging(string value);
    partial void OnPrimaryPhotoExtensionChanged();
    #endregion
		
		public Building()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BuildingId", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long BuildingId
		{
			get
			{
				return this._BuildingId;
			}
			set
			{
				if ((this._BuildingId != value))
				{
					this.OnBuildingIdChanging(value);
					this.SendPropertyChanging();
					this._BuildingId = value;
					this.SendPropertyChanged("BuildingId");
					this.OnBuildingIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDateUtc", DbType="DateTime")]
		public System.Nullable<System.DateTime> CreateDateUtc
		{
			get
			{
				return this._CreateDateUtc;
			}
			set
			{
				if ((this._CreateDateUtc != value))
				{
					this.OnCreateDateUtcChanging(value);
					this.SendPropertyChanging();
					this._CreateDateUtc = value;
					this.SendPropertyChanged("CreateDateUtc");
					this.OnCreateDateUtcChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedBy", DbType="NVarChar(100)")]
		public string CreatedBy
		{
			get
			{
				return this._CreatedBy;
			}
			set
			{
				if ((this._CreatedBy != value))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._CreatedBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateDateUtc", DbType="DateTime")]
		public System.Nullable<System.DateTime> UpdateDateUtc
		{
			get
			{
				return this._UpdateDateUtc;
			}
			set
			{
				if ((this._UpdateDateUtc != value))
				{
					this.OnUpdateDateUtcChanging(value);
					this.SendPropertyChanging();
					this._UpdateDateUtc = value;
					this.SendPropertyChanged("UpdateDateUtc");
					this.OnUpdateDateUtcChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdatedBy", DbType="NVarChar(100)")]
		public string UpdatedBy
		{
			get
			{
				return this._UpdatedBy;
			}
			set
			{
				if ((this._UpdatedBy != value))
				{
					this.OnUpdatedByChanging(value);
					this.SendPropertyChanging();
					this._UpdatedBy = value;
					this.SendPropertyChanged("UpdatedBy");
					this.OnUpdatedByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RibbonId", DbType="NVarChar(50)")]
		public string RibbonId
		{
			get
			{
				return this._RibbonId;
			}
			set
			{
				if ((this._RibbonId != value))
				{
					this.OnRibbonIdChanging(value);
					this.SendPropertyChanging();
					this._RibbonId = value;
					this.SendPropertyChanged("RibbonId");
					this.OnRibbonIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateRibbonActivated", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateRibbonActivated
		{
			get
			{
				return this._DateRibbonActivated;
			}
			set
			{
				if ((this._DateRibbonActivated != value))
				{
					this.OnDateRibbonActivatedChanging(value);
					this.SendPropertyChanging();
					this._DateRibbonActivated = value;
					this.SendPropertyChanged("DateRibbonActivated");
					this.OnDateRibbonActivatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContactInfoId", DbType="Int")]
		public System.Nullable<int> ContactInfoId
		{
			get
			{
				return this._ContactInfoId;
			}
			set
			{
				if ((this._ContactInfoId != value))
				{
					this.OnContactInfoIdChanging(value);
					this.SendPropertyChanging();
					this._ContactInfoId = value;
					this.SendPropertyChanged("ContactInfoId");
					this.OnContactInfoIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address1", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string Address1
		{
			get
			{
				return this._Address1;
			}
			set
			{
				if ((this._Address1 != value))
				{
					this.OnAddress1Changing(value);
					this.SendPropertyChanging();
					this._Address1 = value;
					this.SendPropertyChanged("Address1");
					this.OnAddress1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address2", DbType="NVarChar(250)")]
		public string Address2
		{
			get
			{
				return this._Address2;
			}
			set
			{
				if ((this._Address2 != value))
				{
					this.OnAddress2Changing(value);
					this.SendPropertyChanging();
					this._Address2 = value;
					this.SendPropertyChanged("Address2");
					this.OnAddress2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				if ((this._City != value))
				{
					this.OnCityChanging(value);
					this.SendPropertyChanging();
					this._City = value;
					this.SendPropertyChanged("City");
					this.OnCityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Zip", DbType="NVarChar(10) NOT NULL", CanBeNull=false)]
		public string Zip
		{
			get
			{
				return this._Zip;
			}
			set
			{
				if ((this._Zip != value))
				{
					this.OnZipChanging(value);
					this.SendPropertyChanging();
					this._Zip = value;
					this.SendPropertyChanged("Zip");
					this.OnZipChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Latitude", DbType="Real NOT NULL")]
		public float Latitude
		{
			get
			{
				return this._Latitude;
			}
			set
			{
				if ((this._Latitude != value))
				{
					this.OnLatitudeChanging(value);
					this.SendPropertyChanging();
					this._Latitude = value;
					this.SendPropertyChanged("Latitude");
					this.OnLatitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Longitude", DbType="Real NOT NULL")]
		public float Longitude
		{
			get
			{
				return this._Longitude;
			}
			set
			{
				if ((this._Longitude != value))
				{
					this.OnLongitudeChanging(value);
					this.SendPropertyChanging();
					this._Longitude = value;
					this.SendPropertyChanged("Longitude");
					this.OnLongitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PropertyTypeCode", DbType="Int NOT NULL")]
		public int PropertyTypeCode
		{
			get
			{
				return this._PropertyTypeCode;
			}
			set
			{
				if ((this._PropertyTypeCode != value))
				{
					this.OnPropertyTypeCodeChanging(value);
					this.SendPropertyChanging();
					this._PropertyTypeCode = value;
					this.SendPropertyChanged("PropertyTypeCode");
					this.OnPropertyTypeCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(50)")]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(4000)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SquareFeet", DbType="Int")]
		public System.Nullable<int> SquareFeet
		{
			get
			{
				return this._SquareFeet;
			}
			set
			{
				if ((this._SquareFeet != value))
				{
					this.OnSquareFeetChanging(value);
					this.SendPropertyChanging();
					this._SquareFeet = value;
					this.SendPropertyChanged("SquareFeet");
					this.OnSquareFeetChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Acres", DbType="Real")]
		public System.Nullable<float> Acres
		{
			get
			{
				return this._Acres;
			}
			set
			{
				if ((this._Acres != value))
				{
					this.OnAcresChanging(value);
					this.SendPropertyChanging();
					this._Acres = value;
					this.SendPropertyChanged("Acres");
					this.OnAcresChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_YearBuilt", DbType="Int")]
		public System.Nullable<int> YearBuilt
		{
			get
			{
				return this._YearBuilt;
			}
			set
			{
				if ((this._YearBuilt != value))
				{
					this.OnYearBuiltChanging(value);
					this.SendPropertyChanging();
					this._YearBuilt = value;
					this.SendPropertyChanged("YearBuilt");
					this.OnYearBuiltChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Bedrooms", DbType="Int")]
		public System.Nullable<int> Bedrooms
		{
			get
			{
				return this._Bedrooms;
			}
			set
			{
				if ((this._Bedrooms != value))
				{
					this.OnBedroomsChanging(value);
					this.SendPropertyChanging();
					this._Bedrooms = value;
					this.SendPropertyChanged("Bedrooms");
					this.OnBedroomsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Bathrooms", DbType="Real")]
		public System.Nullable<float> Bathrooms
		{
			get
			{
				return this._Bathrooms;
			}
			set
			{
				if ((this._Bathrooms != value))
				{
					this.OnBathroomsChanging(value);
					this.SendPropertyChanging();
					this._Bathrooms = value;
					this.SendPropertyChanged("Bathrooms");
					this.OnBathroomsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Price", DbType="Decimal(18,2) NOT NULL")]
		public decimal Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if ((this._Price != value))
				{
					this.OnPriceChanging(value);
					this.SendPropertyChanging();
					this._Price = value;
					this.SendPropertyChanged("Price");
					this.OnPriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateAvailableUtc", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateAvailableUtc
		{
			get
			{
				return this._DateAvailableUtc;
			}
			set
			{
				if ((this._DateAvailableUtc != value))
				{
					this.OnDateAvailableUtcChanging(value);
					this.SendPropertyChanging();
					this._DateAvailableUtc = value;
					this.SendPropertyChanged("DateAvailableUtc");
					this.OnDateAvailableUtcChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateActivatedUtc", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateActivatedUtc
		{
			get
			{
				return this._DateActivatedUtc;
			}
			set
			{
				if ((this._DateActivatedUtc != value))
				{
					this.OnDateActivatedUtcChanging(value);
					this.SendPropertyChanging();
					this._DateActivatedUtc = value;
					this.SendPropertyChanged("DateActivatedUtc");
					this.OnDateActivatedUtcChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsActive", DbType="Bit NOT NULL")]
		public bool IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				if ((this._IsActive != value))
				{
					this.OnIsActiveChanging(value);
					this.SendPropertyChanging();
					this._IsActive = value;
					this.SendPropertyChanged("IsActive");
					this.OnIsActiveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsCreditCheckRequired", DbType="Bit NOT NULL")]
		public bool IsCreditCheckRequired
		{
			get
			{
				return this._IsCreditCheckRequired;
			}
			set
			{
				if ((this._IsCreditCheckRequired != value))
				{
					this.OnIsCreditCheckRequiredChanging(value);
					this.SendPropertyChanging();
					this._IsCreditCheckRequired = value;
					this.SendPropertyChanged("IsCreditCheckRequired");
					this.OnIsCreditCheckRequiredChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsBackgroundCheckRequired", DbType="Bit NOT NULL")]
		public bool IsBackgroundCheckRequired
		{
			get
			{
				return this._IsBackgroundCheckRequired;
			}
			set
			{
				if ((this._IsBackgroundCheckRequired != value))
				{
					this.OnIsBackgroundCheckRequiredChanging(value);
					this.SendPropertyChanging();
					this._IsBackgroundCheckRequired = value;
					this.SendPropertyChanged("IsBackgroundCheckRequired");
					this.OnIsBackgroundCheckRequiredChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Deposit", DbType="Decimal(18,2) NOT NULL")]
		public decimal Deposit
		{
			get
			{
				return this._Deposit;
			}
			set
			{
				if ((this._Deposit != value))
				{
					this.OnDepositChanging(value);
					this.SendPropertyChanging();
					this._Deposit = value;
					this.SendPropertyChanged("Deposit");
					this.OnDepositChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RefundableDeposit", DbType="Decimal(18,2) NOT NULL")]
		public decimal RefundableDeposit
		{
			get
			{
				return this._RefundableDeposit;
			}
			set
			{
				if ((this._RefundableDeposit != value))
				{
					this.OnRefundableDepositChanging(value);
					this.SendPropertyChanging();
					this._RefundableDeposit = value;
					this.SendPropertyChanged("RefundableDeposit");
					this.OnRefundableDepositChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LeaseLengthCode", DbType="Int NOT NULL")]
		public int LeaseLengthCode
		{
			get
			{
				return this._LeaseLengthCode;
			}
			set
			{
				if ((this._LeaseLengthCode != value))
				{
					this.OnLeaseLengthCodeChanging(value);
					this.SendPropertyChanging();
					this._LeaseLengthCode = value;
					this.SendPropertyChanged("LeaseLengthCode");
					this.OnLeaseLengthCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsSmokingAllowed", DbType="Bit NOT NULL")]
		public bool IsSmokingAllowed
		{
			get
			{
				return this._IsSmokingAllowed;
			}
			set
			{
				if ((this._IsSmokingAllowed != value))
				{
					this.OnIsSmokingAllowedChanging(value);
					this.SendPropertyChanging();
					this._IsSmokingAllowed = value;
					this.SendPropertyChanged("IsSmokingAllowed");
					this.OnIsSmokingAllowedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ArePetsAllowed", DbType="Bit NOT NULL")]
		public bool ArePetsAllowed
		{
			get
			{
				return this._ArePetsAllowed;
			}
			set
			{
				if ((this._ArePetsAllowed != value))
				{
					this.OnArePetsAllowedChanging(value);
					this.SendPropertyChanging();
					this._ArePetsAllowed = value;
					this.SendPropertyChanged("ArePetsAllowed");
					this.OnArePetsAllowedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsRemovedByAdmin", DbType="Bit NOT NULL")]
		public bool IsRemovedByAdmin
		{
			get
			{
				return this._IsRemovedByAdmin;
			}
			set
			{
				if ((this._IsRemovedByAdmin != value))
				{
					this.OnIsRemovedByAdminChanging(value);
					this.SendPropertyChanging();
					this._IsRemovedByAdmin = value;
					this.SendPropertyChanged("IsRemovedByAdmin");
					this.OnIsRemovedByAdminChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsReported", DbType="Bit NOT NULL")]
		public bool IsReported
		{
			get
			{
				return this._IsReported;
			}
			set
			{
				if ((this._IsReported != value))
				{
					this.OnIsReportedChanging(value);
					this.SendPropertyChanging();
					this._IsReported = value;
					this.SendPropertyChanged("IsReported");
					this.OnIsReportedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReportedText", DbType="NVarChar(1000)")]
		public string ReportedText
		{
			get
			{
				return this._ReportedText;
			}
			set
			{
				if ((this._ReportedText != value))
				{
					this.OnReportedTextChanging(value);
					this.SendPropertyChanging();
					this._ReportedText = value;
					this.SendPropertyChanged("ReportedText");
					this.OnReportedTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDeleted", DbType="Bit NOT NULL")]
		public bool IsDeleted
		{
			get
			{
				return this._IsDeleted;
			}
			set
			{
				if ((this._IsDeleted != value))
				{
					this.OnIsDeletedChanging(value);
					this.SendPropertyChanging();
					this._IsDeleted = value;
					this.SendPropertyChanged("IsDeleted");
					this.OnIsDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HasPriority", DbType="Bit NOT NULL")]
		public bool HasPriority
		{
			get
			{
				return this._HasPriority;
			}
			set
			{
				if ((this._HasPriority != value))
				{
					this.OnHasPriorityChanging(value);
					this.SendPropertyChanging();
					this._HasPriority = value;
					this.SendPropertyChanged("HasPriority");
					this.OnHasPriorityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DatePrioritized", DbType="DateTime")]
		public System.Nullable<System.DateTime> DatePrioritized
		{
			get
			{
				return this._DatePrioritized;
			}
			set
			{
				if ((this._DatePrioritized != value))
				{
					this.OnDatePrioritizedChanging(value);
					this.SendPropertyChanging();
					this._DatePrioritized = value;
					this.SendPropertyChanged("DatePrioritized");
					this.OnDatePrioritizedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PetFee", DbType="Decimal(18,2) NOT NULL")]
		public decimal PetFee
		{
			get
			{
				return this._PetFee;
			}
			set
			{
				if ((this._PetFee != value))
				{
					this.OnPetFeeChanging(value);
					this.SendPropertyChanging();
					this._PetFee = value;
					this.SendPropertyChanged("PetFee");
					this.OnPetFeeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TemporaryOrderId", DbType="Int")]
		public System.Nullable<int> TemporaryOrderId
		{
			get
			{
				return this._TemporaryOrderId;
			}
			set
			{
				if ((this._TemporaryOrderId != value))
				{
					this.OnTemporaryOrderIdChanging(value);
					this.SendPropertyChanging();
					this._TemporaryOrderId = value;
					this.SendPropertyChanged("TemporaryOrderId");
					this.OnTemporaryOrderIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrimaryPhotoId", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> PrimaryPhotoId
		{
			get
			{
				return this._PrimaryPhotoId;
			}
			set
			{
				if ((this._PrimaryPhotoId != value))
				{
					this.OnPrimaryPhotoIdChanging(value);
					this.SendPropertyChanging();
					this._PrimaryPhotoId = value;
					this.SendPropertyChanged("PrimaryPhotoId");
					this.OnPrimaryPhotoIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrimaryPhotoExtension", DbType="NVarChar(MAX)")]
		public string PrimaryPhotoExtension
		{
			get
			{
				return this._PrimaryPhotoExtension;
			}
			set
			{
				if ((this._PrimaryPhotoExtension != value))
				{
					this.OnPrimaryPhotoExtensionChanging(value);
					this.SendPropertyChanging();
					this._PrimaryPhotoExtension = value;
					this.SendPropertyChanged("PrimaryPhotoExtension");
					this.OnPrimaryPhotoExtensionChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
