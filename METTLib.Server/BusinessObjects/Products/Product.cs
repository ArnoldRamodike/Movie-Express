// Generated 11 Nov 2021 13:46 - Singular Systems Object Generator Version 2.2.694
//<auto-generated/>
using System;
using Csla;
using Csla.Serialization;
using Csla.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singular;
using System.Data;
using System.Data.SqlClient;
using MELib.Categories;


namespace MELib.Products
{
    [Serializable]
    public class Product
     : MEBusinessBase<Product>
    {
        #region " Properties and Methods "

        #region " Properties "

        public static PropertyInfo<int> ProductIDProperty = RegisterProperty<int>(c => c.ProductID, "ID", 0);
        /// <summary>
        /// Gets the ID value
        /// </summary>
        [Display(AutoGenerateField = false), Key]
        public int ProductID
        {
            get { return GetProperty(ProductIDProperty); }
        }

        public static PropertyInfo<String> ProductNameProperty = RegisterProperty<String>(c => c.ProductName, "Product Name", "");
        /// <summary>
        /// Gets and sets the Product Name value
        /// </summary>
        [Display(Name = "Product Name", Description = ""),
        StringLength(200, ErrorMessage = "Product Name cannot be more than 200 characters")]
        public String ProductName
        {
            get { return GetProperty(ProductNameProperty); }
            set { SetProperty(ProductNameProperty, value); }
        }

        public static PropertyInfo<String> ProductDescriptionProperty = RegisterProperty<String>(c => c.ProductDescription, "Product Description", "");
        /// <summary>
        /// Gets and sets the Product Description value
        /// </summary>
        [Display(Name = "Product Description", Description = "")]
        public String ProductDescription
        {
            get { return GetProperty(ProductDescriptionProperty); }
            set { SetProperty(ProductDescriptionProperty, value); }
        }

        public static PropertyInfo<Decimal> PriceProperty = RegisterProperty<Decimal>(c => c.Price, "Price", 0D);
        /// <summary>
        /// Gets and sets the Price value
        /// </summary>
        [Display(Name = "Price", Description = ""),
        Required(ErrorMessage = "Price required")]
        public Decimal Price
        {
            get { return GetProperty(PriceProperty); }
            set { SetProperty(PriceProperty, value); }
        }

        public static PropertyInfo<int?> CartegoryIDProperty = RegisterProperty<int?>(c => c.CartegoryID, "Cartegory", null);
        /// <summary>
        /// Gets and sets the Cartegory value
        /// </summary>
        [Display(Name = "Cartegory", Description = "Product Category"),
        Singular.DataAnnotations.DropDownWeb(typeof(CategoryList), DisplayFunction = "CategoryName", ValueMember = "CategoryID"),
        Required(ErrorMessage = "Cartegory required")]
        public int? CartegoryID
        {
            get { return GetProperty(CartegoryIDProperty); }
            set { SetProperty(CartegoryIDProperty, value); }
        }

        public static PropertyInfo<int> QuantityProperty = RegisterProperty<int>(c => c.Quantity, "Quantity", 0);
        /// <summary>
        /// Gets and sets the Quantity value
        /// </summary>
        [Display(Name = "Quantity", Description = ""),
        Required(ErrorMessage = "Quantity required")]
        public int Quantity
        {
            get { return GetProperty(QuantityProperty); }
            set { SetProperty(QuantityProperty, value); }
        }

        public static PropertyInfo<int> UserQuantityProperty = RegisterProperty<int>(c => c.UserQuantity, "User Quantity", 0);
        /// <summary>
        /// Gets and sets the User Quantity value
        /// </summary>
        [Display(Name = "User Quantity", Description = "")]
        public int UserQuantity
        {
            get { return GetProperty(UserQuantityProperty); }
            set { SetProperty(UserQuantityProperty, value); }
        }

        public static PropertyInfo<Boolean> IsActiveIndProperty = RegisterProperty<Boolean>(c => c.IsActiveInd, "Is Active", false);
        /// <summary>
        /// Gets and sets the Is Active value
        /// </summary>
        [Display(Name = "Is Active", Description = ""),
        Required(ErrorMessage = "Is Active required")]
        public Boolean IsActiveInd
        {
            get { return GetProperty(IsActiveIndProperty); }
            set { SetProperty(IsActiveIndProperty, value); }
        }

        public static PropertyInfo<DateTime?> DeletedDateProperty = RegisterProperty<DateTime?>(c => c.DeletedDate, "Deleted Date");
        /// <summary>
        /// Gets and sets the Deleted Date value
        /// </summary>
        [Display(Name = "Deleted Date", Description = "")]
        public DateTime? DeletedDate
        {
            get
            {
                return GetProperty(DeletedDateProperty);
            }
            set
            {
                SetProperty(DeletedDateProperty, value);
            }
        }

        public static PropertyInfo<int> DeletedByProperty = RegisterProperty<int>(c => c.DeletedBy, "Deleted By", 0);
        /// <summary>
        /// Gets and sets the Deleted By value
        /// </summary>
        [Display(Name = "Deleted By", Description = "")]
        public int DeletedBy
        {
            get { return GetProperty(DeletedByProperty); }
            set { SetProperty(DeletedByProperty, value); }
        }

        public static PropertyInfo<SmartDate> CreatedDateProperty = RegisterProperty<SmartDate>(c => c.CreatedDate, "Created Date", new SmartDate(DateTime.Now));
        /// <summary>
        /// Gets the Created Date value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public SmartDate CreatedDate
        {
            get { return GetProperty(CreatedDateProperty); }
        }

        public static PropertyInfo<int> CreatedByProperty = RegisterProperty<int>(c => c.CreatedBy, "Created By", 0);
        /// <summary>
        /// Gets the Created By value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int CreatedBy
        {
            get { return GetProperty(CreatedByProperty); }
        }

        public static PropertyInfo<SmartDate> ModifiedDateProperty = RegisterProperty<SmartDate>(c => c.ModifiedDate, "Modified Date", new SmartDate(DateTime.Now));
        /// <summary>
        /// Gets the Modified Date value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public SmartDate ModifiedDate
        {
            get { return GetProperty(ModifiedDateProperty); }
        }

        public static PropertyInfo<int> ModifiedByProperty = RegisterProperty<int>(c => c.ModifiedBy, "Modified By", 0);
        /// <summary>
        /// Gets the Modified By value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int ModifiedBy
        {
            get { return GetProperty(ModifiedByProperty); }
        }

        #endregion

        #region " Methods "

        protected override object GetIdValue()
        {
            return GetProperty(ProductIDProperty);
        }

        public override string ToString()
        {
            if (this.ProductName.Length == 0)
            {
                if (this.IsNew)
                {
                    return String.Format("New {0}", "Product");
                }
                else
                {
                    return String.Format("Blank {0}", "Product");
                }
            }
            else
            {
                return this.ProductName;
            }
        }

        #endregion

        #endregion

        #region " Validation Rules "

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
        }

        #endregion

        #region " Data Access & Factory Methods "

        protected override void OnCreate()
        {
            // This is called when a new object is created
            // Set any variables here, not in the constructor or NewProduct() method.
        }

        public static Product NewProduct()
        {
            return DataPortal.CreateChild<Product>();
        }

        public Product()
        {
            MarkAsChild();
        }

        internal static Product GetProduct(SafeDataReader dr)
        {
            var p = new Product();
            p.Fetch(dr);
            return p;
        }

        protected void Fetch(SafeDataReader sdr)
        {
            using (BypassPropertyChecks)
            {
                int i = 0;
                LoadProperty(ProductIDProperty, sdr.GetInt32(i++));
                LoadProperty(ProductNameProperty, sdr.GetString(i++));
                LoadProperty(ProductDescriptionProperty, sdr.GetString(i++));
                LoadProperty(PriceProperty, sdr.GetDecimal(i++));
                LoadProperty(CartegoryIDProperty, Singular.Misc.ZeroNothing(sdr.GetInt32(i++)));
                LoadProperty(QuantityProperty, sdr.GetInt32(i++));
                LoadProperty(UserQuantityProperty, sdr.GetInt32(i++));
                LoadProperty(IsActiveIndProperty, sdr.GetBoolean(i++));
                LoadProperty(DeletedDateProperty, sdr.GetValue(i++));
                LoadProperty(DeletedByProperty, sdr.GetInt32(i++));
                LoadProperty(CreatedDateProperty, sdr.GetSmartDate(i++));
                LoadProperty(CreatedByProperty, sdr.GetInt32(i++));
                LoadProperty(ModifiedDateProperty, sdr.GetSmartDate(i++));
                LoadProperty(ModifiedByProperty, sdr.GetInt32(i++));
            }

            MarkAsChild();
            MarkOld();
            BusinessRules.CheckRules();
        }

        protected override Action<SqlCommand> SetupSaveCommand(SqlCommand cm)
        {
            LoadProperty(ModifiedByProperty, Settings.CurrentUser.UserID);

            AddPrimaryKeyParam(cm, ProductIDProperty);

            cm.Parameters.AddWithValue("@ProductName", GetProperty(ProductNameProperty));
            cm.Parameters.AddWithValue("@ProductDescription", GetProperty(ProductDescriptionProperty));
            cm.Parameters.AddWithValue("@Price", GetProperty(PriceProperty));
            cm.Parameters.AddWithValue("@CartegoryID", GetProperty(CartegoryIDProperty));
            cm.Parameters.AddWithValue("@Quantity", GetProperty(QuantityProperty));
            cm.Parameters.AddWithValue("@UserQuantity", GetProperty(UserQuantityProperty));
            cm.Parameters.AddWithValue("@IsActiveInd", GetProperty(IsActiveIndProperty));
            cm.Parameters.AddWithValue("@DeletedDate", Singular.Misc.NothingDBNull(DeletedDate));
            cm.Parameters.AddWithValue("@DeletedBy", GetProperty(DeletedByProperty));
            cm.Parameters.AddWithValue("@ModifiedBy", GetProperty(ModifiedByProperty));

            return (scm) =>
            {
    // Post Save
    if (this.IsNew)
                {
                    LoadProperty(ProductIDProperty, scm.Parameters["@ProductID"].Value);
                }
            };
        }

        protected override void SaveChildren()
        {
            // No Children
        }

        protected override void SetupDeleteCommand(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@ProductID", GetProperty(ProductIDProperty));
        }

        #endregion

    }

}