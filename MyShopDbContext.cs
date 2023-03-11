using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

#nullable disable

namespace ShopDb
{
    public partial class MyShopDbContext : DbContext
    {
        public MyShopDbContext()
        {
        }

        IConfiguration _config;
        public MyShopDbContext(DbContextOptions<MyShopDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
            Console.WriteLine("MyshopContext ---------------------- statr60.05.21");
            Database.SetCommandTimeout(300);
            //Database.EnsureDeleted();  //03.13.20
            Database.EnsureCreated();
        }

        public virtual DbSet<ImageP> ImagePs { get; set; }
        public virtual DbSet<KatalogP> KatalogPs { get; set; }
        public virtual DbSet<MaterialP> MaterialPs { get; set; }
        public virtual DbSet<CategoriaP> CategoriaPs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductNomenclature> ProductNomenclatures { get; set; }
        public virtual DbSet<Nomenclature> Nomenclatures { get; set; }
        public virtual DbSet<KatalogN> KatalogNs { get; set; }
        public virtual DbSet<BrandN> BrandNs { get; set; }
        public virtual DbSet<ArticleN> ArticleNs { get; set; }
        public virtual DbSet<ColorN> ColorNs { get; set; }
        public virtual DbSet<CategoriaN> CategoriaNs { get; set; }
        public virtual DbSet<PostavchikN> PostavchikNs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //#warning
            //To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //18.09.21  string contString = _config["ConnectStringLocal"];
            //18.09.21    optionsBuilder.UseMySql(contString, new MySqlServerVersion(new Version(8, 0, 11)));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<ImageP>(entity =>
            {
                entity.ToTable("ImageP");

                entity.HasIndex(e => e.ProductId, "fk_Image_Product1_idx");

                entity.HasIndex(e => e.Name, "name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Image_Product1");
            });

            modelBuilder.Entity<KatalogP>(entity =>
            {
                entity.ToTable("KatalogP");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");
                entity.Property(p => p.Flag_link).HasColumnType("tinyint(1)");
                entity.Property(p => p.Flag_href).HasColumnType("tinyint(1)");
                entity.Property(p => p.Link).HasColumnName("link")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(p => p.DecriptSEO).HasColumnName("decriptSEO")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");
                entity.Property(p => p.KeywordsSEO).HasColumnName("keywordsSEO")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");
            });

            modelBuilder.Entity<MaterialP>(entity =>
            {
                entity.ToTable("MaterialP");

                entity.HasIndex(e => e.Name, "name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("name")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");


                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");

            });

            modelBuilder.Entity<CategoriaP>(entity =>
            {
                entity.ToTable("CategoriaP");

                entity.HasIndex(e => e.Name, "name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("name")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");


                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.KatalogId, "fk_Product_Katalog1_idx");

                entity.HasIndex(e => e.MaterialId, "fk_Product_Material1_idx");

                entity.HasIndex(e => e.CategoriaId, "fk_Product_Categoria1_idx");

                entity.HasIndex(e => new {e.KatalogId, e.Name}, "name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.ImageGuid)
                    .HasMaxLength(36)
                    .HasColumnName("image")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.KatalogId).HasColumnName("Katalog_id");

                entity.Property(e => e.Markup)
                    .HasColumnName("markup")
                    .HasComment("торговая наценка");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(p => p.MaterialId).HasColumnName("material_id");
                entity.Property(p => p.CategoriaId).HasColumnName("categoria_id");

                entity.HasOne(d => d.Katalog)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.KatalogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Katalog1");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Material1");

                entity.HasOne(d => d.Categoria)
                  .WithMany(p => p.Products)
                  .HasForeignKey(d => d.CategoriaId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_Product_Categoria1");

            });

            modelBuilder.Entity<PostavchikN>(entity =>
            {
                entity.ToTable("PostavchikN");

               
                entity.HasIndex(e => e.Name, "name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("name")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");

                entity.Property(e => e.NormalizedName)
                 .IsRequired()
                 .HasMaxLength(100)
                 .HasColumnName("normalizedName")
                 .UseCollation("utf8_general_ci")
                 .HasCharSet("utf8");




                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");

            });

            modelBuilder.Entity<CategoriaN>(entity =>
            {
                entity.ToTable("CategoriaN");

                entity.HasIndex(e => new {e.PostavchikId, e.Name }, "name_UNIQUE")
                   .IsUnique();
                entity.HasIndex(e => e.PostavchikId, "fk_CategoriaN_PostavchikN1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");
                entity.Property(p => p.PostavchikId).HasColumnName("postavchik_id");

                entity.Property(p => p.Flag_link).HasColumnType("tinyint(1)");
                entity.Property(p => p.Flag_href).HasColumnType("tinyint(1)");
                entity.Property(p => p.Link).HasColumnName("link");

                entity.Property(p => p.DecriptSEO).HasColumnName("decriptSEO")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");


                entity.HasOne(d => d.Postavchik)
              .WithMany(p => p.CategoriaNs)
              .HasForeignKey(d => d.PostavchikId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_CategoriaN_PostavchikN1");

            });
           
            modelBuilder.Entity<BrandN>(entity =>
            {
                entity.ToTable("BrandN");

                entity.HasIndex(e => new { e.PostavchikId, e.Name }, "name_UNIQUE")
                   .IsUnique();
                entity.HasIndex(e => e.PostavchikId, "fk_Brand_PostavchikN1_idx");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(p => p.PostavchikId).HasColumnName("postavchik_id");              

                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("name")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");

                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Postavchik)
                .WithMany(p => p.BrandNs)
                .HasForeignKey(d => d.PostavchikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_BrandN_PostavchikN1");

            });
            modelBuilder.Entity<ColorN>(entity =>
            {
                entity.ToTable("ColorN");

                entity.HasIndex(e => new { e.PostavchikId, e.Name }, "name_UNIQUE")
                  .IsUnique();
                entity.HasIndex(e => e.PostavchikId, "fk_Color_PostavchikN1_idx");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(p => p.PostavchikId).HasColumnName("postavchik_id");
              
                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("name")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");

                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Postavchik)
                 .WithMany(p => p.ColorNs)
                 .HasForeignKey(d => d.PostavchikId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("fk_ColorN_PostavchikN1");

            });
            modelBuilder.Entity<ArticleN>(entity =>
            {
                entity.ToTable("ArticleN");

                entity.HasIndex(e => new { e.PostavchikId, e.Name }, "name_UNIQUE")
                .IsUnique();
                entity.HasIndex(e => e.PostavchikId, "fk_Article_PostavchikN1_idx");
               
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(p => p.PostavchikId).HasColumnName("postavchik_id");
               


                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("name")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");

                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Postavchik)
                  .WithMany(p => p.ArticleNs)
                  .HasForeignKey(d => d.PostavchikId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_ArticleN_PostavchikN1");

            });
            modelBuilder.Entity<KatalogN>(entity =>
            {
                entity.ToTable("KatalogN");

                entity.HasIndex(e => new { e.PostavchikId, e.Name }, "name_UNIQUE")
                .IsUnique();
                entity.HasIndex(e => e.PostavchikId, "fk_KatalogN_PostavchikN1_idx");
                entity.HasIndex(e => e.CategoriaId, "fk_KatalogN_CategoriaN1_idx");

                entity.Property(e => e.Id).HasColumnName("id");
               
                entity.Property(p => p.PostavchikId).HasColumnName("postavchik_id");

                entity.Property(p => p.CategoriaId).HasColumnName("categoria_id");               

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(p => p.Hidden).HasColumnType("tinyint(1)");

                entity.Property(p => p.DecriptSEO).HasColumnName("decriptSEO")
                   .UseCollation("utf8_general_ci")
                   .HasCharSet("utf8");

                entity.HasOne(d => d.Categoria)
                 .WithMany(p => p.Katalogs)
                 .HasForeignKey(d => d.CategoriaId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("fk_KatalogN_CategoriaN1");

                entity.HasOne(d => d.Postavchik)
                  .WithMany(p => p.KatalogNs)
                  .HasForeignKey(d => d.PostavchikId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_KatalogN_PostavchikN1");

            });
            modelBuilder.Entity<Nomenclature>(entity =>
            {
                entity.ToTable("Nomenclature");

                entity.HasIndex(e => new { e.PostavchikId,e.KatalogId,e.ColorId,e.ArticleId,e.BrandId, e.Name }, "name_UNIQUE")
              .IsUnique();
                entity.HasIndex(e => e.PostavchikId, "fk_Nomenclature_PostavchikN1_idx");

                entity.HasIndex(e => e.KatalogId, "fk_Nomenclature_KatalogN1_idx");

                entity.HasIndex(e => e.ColorId, "fk_Nomenclature_Color1_idx");

                entity.HasIndex(e => e.ArticleId, "fk_Nomenclature_Article1_idx");

                entity.HasIndex(e => e.BrandId, "fk_Nomenclature_Brand1_idx");

                entity.Property(p => p.KatalogId).HasColumnName("katalog_id");
                entity.Property(p => p.ColorId).HasColumnName("color_id");
                entity.Property(p => p.ArticleId).HasColumnName("article_id");
                entity.Property(p => p.BrandId).HasColumnName("brand_id");
                entity.Property(p => p.PostavchikId).HasColumnName("postavchik_id");

                entity.HasOne(d => d.Katalog)
                   .WithMany(p => p.Nomenclatures)
                   .HasForeignKey(d => d.KatalogId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_Nomenclature_KatalogN1");

                entity.HasOne(d => d.Color)
                 .WithMany(p => p.Nomenclatures)
                 .HasForeignKey(d => d.ColorId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("fk_Nomenclature_Color1");

                entity.HasOne(d => d.Article)
                .WithMany(p => p.Nomenclatures)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Nomenclature_Article1");

                entity.HasOne(d => d.Brand)
              .WithMany(p => p.Nomenclatures)
              .HasForeignKey(d => d.BrandId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_Nomenclature_Brand1");

                entity.HasOne(d => d.Postavchik)
                   .WithMany(p => p.Nomenclatures)
                   .HasForeignKey(d => d.PostavchikId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_Nomenclature_PostavchikN1");



                entity.HasIndex(e => new { e.ArticleId,e.BrandId, e.KatalogId, e.Name }, "articleId_katalogId_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Guid)
                    .HasMaxLength(36)
                    .HasColumnName("guid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8"); // 30.03.22

                entity.Property(p => p.Hidden)
                .HasColumnName("hidden")
                .HasColumnType("tinyint(1)");

                entity.Property(p => p.InStock)
               .HasColumnName("inStock")
               .HasColumnType("tinyint(0)");

                entity.Property(p => p.Sale)
               .HasColumnName("sale")
               .HasColumnType("tinyint(1)");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Markup)
                    .HasColumnName("markup")
                    .HasComment("торговая наценка");

                entity.Property(n => n.Position)
                .HasColumnName("position").HasDefaultValue(0);



                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");




            });

            modelBuilder.Entity<ProductNomenclature>(entity =>
            {
                entity.ToTable("ProductNomenclature");

                entity.HasIndex(e => e.NomenclatureId, "fk_ProductNomenclature_Nomenclature1_idx");

                entity.HasIndex(e => e.ProductId, "fk_ProductNomenclature_Product1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NomenclatureId).HasColumnName("Nomenclature_id");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.HasOne(d => d.Nomenclature)
                    .WithMany(p => p.ProductNomenclatures)
                    .HasForeignKey(d => d.NomenclatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ProductNomenclature_Nomenclature1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductNomenclatures)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ProductNomenclature_Product1");
            });





            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            OnModelKatalogCreating(modelBuilder);
            OnModelCategoriaPCreating(modelBuilder);
            OnModelMaterialPCreating(modelBuilder);
            OnModelPostavchikNCreating(modelBuilder);
            OnModelCategoriaNCreating(modelBuilder);
            OnModelKatalogNCreating(modelBuilder);
            OnModelBrandNCreating(modelBuilder);
            OnModelColorNCreating(modelBuilder);
            OnModelArticleNCreating(modelBuilder);


        }

        private void OnModelKatalogCreating(ModelBuilder modelBuilder)
        {



            var kagalog = new KatalogP[]{
        new KatalogP {Id=1,Name="Комод",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
         new KatalogP{Id=2,Name="Кровать",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
          new KatalogP{Id=3,Name="Шкаф",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
          new KatalogP{Id=4,Name="Кухонный Уголок",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
          new KatalogP{Id=5,Name="Стол Обеденный",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""} ,
          new KatalogP{Id=6,Name="Стол Писменный",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
          new KatalogP{Id=7,Name="Стол Журнальный",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
          new KatalogP{Id=8,Name="Стол Маникюрный",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
          new KatalogP{Id=9,Name="Стол Тумба",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
          new KatalogP{Id=10,Name="Кухня",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""},
          new KatalogP{Id=11,Name="Комплектующие",Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO="",KeywordsSEO=""}

            };

            //  Console.WriteLine("Create -----------      ProductType()---------- Start->");

            modelBuilder.Entity<KatalogP>().HasData(kagalog);
            base.OnModelCreating(modelBuilder);
        }

        private void OnModelMaterialPCreating(ModelBuilder modelBuilder)
        {


            var typeProduct = new MaterialP[]{
                 new MaterialP{Id=1,Name="All",Description="",Hidden=false},
                 new MaterialP{Id=2,Name="ЛДСП",Description="",Hidden=false},
                 new MaterialP{Id=3,Name="МДФ",Description="",Hidden=false}
             };
            modelBuilder.Entity<MaterialP>().HasData(typeProduct);
            base.OnModelCreating(modelBuilder);

        }

        private void OnModelCategoriaPCreating(ModelBuilder modelBuilder)
        {
            var categorias = new CategoriaP[]{
                 new CategoriaP{Id=1,Name="All",Description="Категория не Определена",Hidden=false},
                 new CategoriaP{Id=2,Name="корпусная мебель",Description="Стол Шкаф Камод Кухня",Hidden=false},
                  new CategoriaP{Id=3,Name="мягкая мебель",Description="Стол Шкаф Камод Кухня",Hidden=false}

             };
            modelBuilder.Entity<CategoriaP>().HasData(categorias);
            base.OnModelCreating(modelBuilder);

        }
        //------------- nomenklature -------------
        private void OnModelPostavchikNCreating(ModelBuilder modelBuilder)
        {
            var postavchik = new PostavchikN[]{
                 new PostavchikN{Id=1,Name="fornityreO",NormalizedName="Форнирура", Hidden=false},
                 new PostavchikN{Id=2,Name="tkanO",NormalizedName="Ткань",Hidden=false},
                 new PostavchikN{Id=3,Name="stekloM",NormalizedName = "Стекла", Hidden=false},
                  new PostavchikN{Id=4,Name="matrasX",NormalizedName = "Матрасы", Hidden=false},
                   new PostavchikN{Id=5,Name="LaminatSH",NormalizedName = "Матрасы", Hidden=false}
             };
            modelBuilder.Entity<PostavchikN>().HasData(postavchik);
            base.OnModelCreating(modelBuilder);

        }

        private void OnModelCategoriaNCreating(ModelBuilder modelBuilder)
        {
            var categorias = new CategoriaN[]{
        new  CategoriaN {Id=1,Name="Функциональная фурнитура",PostavchikId=1, Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO=""},
        new  CategoriaN{Id=2,Name="Лицевая фурнитура",PostavchikId=1,Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO=""},
         new  CategoriaN{Id=3,Name="Кухни",PostavchikId=1,Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO=""},
         new  CategoriaN{Id=4,Name="Шкаф-купе",PostavchikId=1,Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO=""},
         new  CategoriaN{Id=5,Name="Кровати",PostavchikId=1,Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO=""},
         new  CategoriaN{Id=6,Name="Кромка",PostavchikId=1,Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO=""},
         new  CategoriaN{Id=7,Name="Крепеж",PostavchikId=1,Hidden=false,Flag_href=false,Flag_link=false,DecriptSEO=""},
          new  CategoriaN{Id=8,Name="Встраиваемая техника",PostavchikId=1,Flag_href=false,Flag_link=false,Hidden=false,DecriptSEO=""},

            };
            modelBuilder.Entity<CategoriaN>().HasData(categorias);
            base.OnModelCreating(modelBuilder);
        }


        private void OnModelBrandNCreating(ModelBuilder modelBuilder)
        {


            var brand = new BrandN[]{
                 new BrandN{Id=1,Name="none",PostavchikId=1,Hidden=false},
                 new BrandN{Id=2,Name="Boyard",PostavchikId=1,Hidden=false},
                 new BrandN{Id=3,Name="Blum",PostavchikId = 1, Hidden=false}
             };
            modelBuilder.Entity<BrandN>().HasData(brand);
            base.OnModelCreating(modelBuilder);

        }

        private void OnModelColorNCreating(ModelBuilder modelBuilder)
        {


            var color = new ColorN[]{
                 new ColorN{Id=1,Name="none",PostavchikId=1,Hidden=false},
                 new ColorN{Id=2,Name="Серебро",PostavchikId = 1, Hidden=false},
                 new ColorN{Id=3,Name="Хром",   PostavchikId = 1, Hidden=false},
                 new ColorN{Id=4,Name="Бронза",PostavchikId = 1, Hidden=false}
             };
            modelBuilder.Entity<ColorN>().HasData(color);
            base.OnModelCreating(modelBuilder);

        }

        private void OnModelArticleNCreating(ModelBuilder modelBuilder)
        {


            var article = new ArticleN[]{
                 new ArticleN{Id=1,Name="none",PostavchikId = 1, Hidden=false},
                 new ArticleN{Id=2,Name="96",PostavchikId = 1, Hidden=false},
                 new ArticleN{Id=3,Name="128",PostavchikId = 1, Hidden=false},
                 new ArticleN{Id=4,Name="160",PostavchikId = 1, Hidden=false},
                 new ArticleN{Id=5,Name="350",PostavchikId = 1, Hidden=false},
                 new ArticleN{Id=6,Name="450",PostavchikId = 1, Hidden=false},
                  new ArticleN{Id=7,Name="50",PostavchikId = 1, Hidden=false},
                   new ArticleN{Id=8,Name="60",PostavchikId = 1, Hidden=false},
                    new ArticleN{Id=9,Name="100",PostavchikId = 1, Hidden=false},
                 new ArticleN{Id=10,Name="500",PostavchikId = 1, Hidden=false},
                   new ArticleN{Id=11,Name="600",PostavchikId = 1, Hidden=false},
                    new ArticleN{Id=12,Name="800",PostavchikId = 1, Hidden=false}
             };
            modelBuilder.Entity<ArticleN>().HasData(article);
            base.OnModelCreating(modelBuilder);

        }



        private void OnModelKatalogNCreating(ModelBuilder modelBuilder)
        {
            var kagalog = new KatalogN[]{
        new KatalogN {Id=1,Name="Ручки",PostavchikId=1, CategoriaId=1, Hidden=false,DecriptSEO=""},
        new KatalogN{Id=2,Name="Крючки",PostavchikId = 1, CategoriaId=1,Hidden=false,DecriptSEO=""},
         new KatalogN{Id=3,Name="Направляющие",PostavchikId = 1, CategoriaId=1,Hidden=false,DecriptSEO=""},
         new KatalogN{Id=4,Name="Петли",PostavchikId = 1, CategoriaId=1,Hidden=false,DecriptSEO=""},

            };
            modelBuilder.Entity<KatalogN>().HasData(kagalog);
            base.OnModelCreating(modelBuilder);
        }



        //  при создании бд  создается admin 

    }
}
