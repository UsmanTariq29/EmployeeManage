
using Microsoft.EntityFrameworkCore;


namespace EmployeeManage.Models
{
    public partial class EmployeesDBContext : DbContext
    {
       
        public EmployeesDBContext(DbContextOptions<EmployeesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<TblBranch> TblBranches { get; set; }
        public virtual DbSet<TblCity> TblCities { get; set; }
        public virtual DbSet<TblCompany> TblCompanies { get; set; }
        public virtual DbSet<TblCountry> TblCountries { get; set; }
        public virtual DbSet<TblCustomer> TblCustomers { get; set; }
        public virtual DbSet<TblDepartment> TblDepartments { get; set; }
        public virtual DbSet<TblDocumentCategory> TblDocumentCategories { get; set; }
        public virtual DbSet<TblEmployee> TblEmployees { get; set; }
        public virtual DbSet<TblEmployeeAllowance> TblEmployeeAllowances { get; set; }
        public virtual DbSet<TblEmployeeAttandance> TblEmployeeAttandances { get; set; }
        public virtual DbSet<TblEmployeeDocument> TblEmployeeDocuments { get; set; }
        public virtual DbSet<TblEmployeeManagementLog> TblEmployeeManagementLogs { get; set; }
        public virtual DbSet<TblHoldInvoiceMaster> TblHoldInvoiceMasters { get; set; }
        public virtual DbSet<TblHoldOrderDetail> TblHoldOrderDetails { get; set; }
        public virtual DbSet<TblHoldOrderMaster> TblHoldOrderMasters { get; set; }
        public virtual DbSet<TblInvoiceDetail> TblInvoiceDetails { get; set; }
        public virtual DbSet<TblInvoiceMaster> TblInvoiceMasters { get; set; }
        public virtual DbSet<TblItem> TblItems { get; set; }
        public virtual DbSet<TblItemBarcode> TblItemBarcodes { get; set; }
        public virtual DbSet<TblItemGroup> TblItemGroups { get; set; }
        public virtual DbSet<TblItemPrice> TblItemPrices { get; set; }
        public virtual DbSet<TblItemsPriceLog> TblItemsPriceLogs { get; set; }
        public virtual DbSet<TblPromotionDetail> TblPromotionDetails { get; set; }
        public virtual DbSet<TblPromotionMaster> TblPromotionMasters { get; set; }
        public virtual DbSet<TblPromotionType> TblPromotionTypes { get; set; }
        public virtual DbSet<TblPurchaseDetail> TblPurchaseDetails { get; set; }
        public virtual DbSet<TblPurchaseMaster> TblPurchaseMasters { get; set; }
        public virtual DbSet<TblRegion> TblRegions { get; set; }
        public virtual DbSet<TblSupplier> TblSuppliers { get; set; }
        public virtual DbSet<TblTax> TblTaxes { get; set; }
        public virtual DbSet<TblUnitOfMeasure> TblUnitOfMeasures { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<VwEmployeeDatum> VwEmployeeData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SAFAMISSA;Database=EmployeesDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<TblBranch>(entity =>
            {
                entity.HasKey(e => e.BranchId)
                    .HasName("PK_TblBranches");

                entity.ToTable("TblBranch");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.BranchCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BranchGuid)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCity>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("TblCity");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("TblCompany");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CompanyAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyGuid)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("TblCountry");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("TblCustomer");

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BranchGUID");

                entity.Property(e => e.Cnicnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CNICNumber");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CompanyGUID");

                entity.Property(e => e.CreatedByUserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CreatedByUserGUID");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ntnnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NTNNumber");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("TblDepartment");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblDocumentCategory>(entity =>
            {
                entity.HasKey(e => e.DocumentCategoryId);

                entity.ToTable("TblDocumentCategory");

                entity.Property(e => e.DocumentCategoryId).HasColumnName("DocumentCategoryID");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("TblEmployee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GrossSalary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.Property(e => e.NationalityId).HasColumnName("NationalityID");

                entity.Property(e => e.PhotoPath)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEmployeeAllowance>(entity =>
            {
                entity.HasKey(e => e.AllowanceId)
                    .HasName("PK_EmployeeAllowances");

                entity.ToTable("TblEmployeeAllowance");

                entity.Property(e => e.AllowanceId).HasColumnName("AllowanceID");

                entity.Property(e => e.AllowanceDetail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblEmployeeAllowances)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblEmployeeAllowances_TblEmployee");
            });

            modelBuilder.Entity<TblEmployeeAttandance>(entity =>
            {
                entity.HasKey(e => e.AttandanceId);

                entity.ToTable("TblEmployeeAttandance");

                entity.Property(e => e.AttandanceId).HasColumnName("AttandanceID");

                entity.Property(e => e.AttandanceDate).HasColumnType("date");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.TimeIn).HasColumnName("TimeIN");
            });

            modelBuilder.Entity<TblEmployeeDocument>(entity =>
            {
                entity.HasKey(e => e.EmployeeDocumentId)
                    .HasName("PK_TblEmployeesDocument");

                entity.ToTable("TblEmployeeDocument");

                entity.Property(e => e.EmployeeDocumentId).HasColumnName("EmployeeDocumentID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DocumentCategoryId).HasColumnName("DocumentCategoryID");

                entity.Property(e => e.DocumentPath)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.ExpireDate).HasColumnType("date");

                entity.Property(e => e.Remarks).HasMaxLength(250);
            });

            modelBuilder.Entity<TblEmployeeManagementLog>(entity =>
            {
                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblHoldInvoiceMaster>(entity =>
            {
                entity.HasKey(e => e.HoldOrderMasterId);

                entity.ToTable("TblHold_InvoiceMaster");

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BranchGUID");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CompanyGUID");

                entity.Property(e => e.CreatedByUserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CreatedByUserGUID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<TblHoldOrderDetail>(entity =>
            {
                entity.HasKey(e => e.HoldOrderDetailId)
                    .HasName("PK_TblHold_invoiceDetail");

                entity.ToTable("TblHoldOrderDetail");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<TblHoldOrderMaster>(entity =>
            {
                entity.HasKey(e => e.HoldInvoiceMasterId);

                entity.ToTable("TblHoldOrderMaster");

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BranchGUID");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CompanyGUID");

                entity.Property(e => e.CreatedByUserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CreatedByUserGUID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<TblInvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.InvoiceDetailId);

                entity.ToTable("TblInvoiceDetail");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<TblInvoiceMaster>(entity =>
            {
                entity.HasKey(e => e.InvoiceMasterId)
                    .HasName("PK_Invoice_Master_Detail");

                entity.ToTable("TblInvoiceMaster");

                entity.Property(e => e.AmountReceived).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Balance).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BranchGUID");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CompanyGUID");

                entity.Property(e => e.CreatedByUserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CreatedByUserGUID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<TblItem>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemDateTime).HasColumnType("datetime");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserGuId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblItemBarcode>(entity =>
            {
                entity.HasKey(e => e.BarcodeId)
                    .HasName("PK_TblBarcode");

                entity.ToTable("TblItemBarcode");

                entity.Property(e => e.ItemBarcode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblItemGroup>(entity =>
            {
                entity.HasKey(e => e.ItemGroupId)
                    .HasName("PK_TblItem_Group");

                entity.ToTable("TblItemGroup");

                entity.HasIndex(e => e.ItemGroupName, "UQ__TblItem___0141EE924FE72013")
                    .IsUnique();

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ItemGroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserGuId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblItemPrice>(entity =>
            {
                entity.HasKey(e => e.PriceId);

                entity.ToTable("TblItemPrice");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CompanyGUID");

                entity.Property(e => e.ItemsPrice).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.UserGuid)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("UserGUID");
            });

            modelBuilder.Entity<TblItemsPriceLog>(entity =>
            {
                entity.ToTable("TblItemsPriceLog");

                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPromotionDetail>(entity =>
            {
                entity.HasKey(e => e.PromotionDetailId);

                entity.ToTable("TblPromotionDetail");

                entity.Property(e => e.PromotionPercentage).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TblPromotionMaster>(entity =>
            {
                entity.HasKey(e => e.PromotionId);

                entity.ToTable("TblPromotionMaster");

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BranchGUID");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CompanyGUID");

                entity.Property(e => e.CreatedByUserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CreatedByUserGUID");

                entity.Property(e => e.PromotionDescription)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PromotionEndDate).HasColumnType("datetime");

                entity.Property(e => e.PromotionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PromotionPercentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PromotionStartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPromotionType>(entity =>
            {
                entity.HasKey(e => e.PromotionTypeId);

                entity.ToTable("TblPromotionType");

                entity.Property(e => e.PromotionType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPurchaseDetail>(entity =>
            {
                entity.HasKey(e => e.PurchaseDetailId);

                entity.ToTable("TblPurchaseDetail");

                entity.Property(e => e.PurchaseDetailId).HasColumnName("purchaseDetailId");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.AmountAfterDiscount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FixedDiscount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Percentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<TblPurchaseMaster>(entity =>
            {
                entity.HasKey(e => e.PurchaseMasterId);

                entity.ToTable("TblPurchaseMaster");

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BranchGUID");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CompanyGUID");

                entity.Property(e => e.CreatedByUserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CreatedByUserGUID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.InvoiceNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalTax).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<TblRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.ToTable("TblRegion");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.ToTable("TblSupplier");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BranchGUID");

                entity.Property(e => e.Cnic)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CNIC");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CompanyGUID");

                entity.Property(e => e.CreatedByUserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CreatedByUserGUID");

                entity.Property(e => e.DetailCodeId).HasColumnName("DetailCodeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubCodeId).HasColumnName("SubCodeID");
            });

            modelBuilder.Entity<TblTax>(entity =>
            {
                entity.HasKey(e => e.TaxId);

                entity.ToTable("TblTax");

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("branchGUID");

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("companyGUID");

                entity.Property(e => e.CreatedByUserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CreatedByUserGUID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Percentage).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TblUnitOfMeasure>(entity =>
            {
                entity.HasKey(e => e.UnitOfMeasureId);

                entity.ToTable("TblUnitOfMeasure");

                entity.HasIndex(e => e.UnitOfMeasure, "UQ__TblUnitO__2F34111F3B588796")
                    .IsUnique();

                entity.Property(e => e.UnitOfMeasure)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnitOfMeasureDescription)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("TblUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Passwordexp).HasColumnType("datetime");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserGuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwEmployeeDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwEmployeeData");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoPath)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
