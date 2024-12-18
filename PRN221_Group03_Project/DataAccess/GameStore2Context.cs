using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class GameStore2Context : DbContext
    {
        public GameStore2Context()
        {
        }

        public GameStore2Context(DbContextOptions<GameStore2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<MoneyHistory> MoneyHistories { get; set; }

        public virtual DbSet<MoneyManagement> MoneyManagements { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=DESKTOP-DNH3PN2\\SQLEXPRESS;uid=sa;pwd=30102003;database=GameStore;Encrypt=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Cid);

                entity.ToTable("Category");

                entity.Property(e => e.Cid)
                    .HasMaxLength(50)
                    .HasColumnName("CID");
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Gid });

                entity.ToTable("Feedback");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
                entity.Property(e => e.Gid)
                    .HasMaxLength(50)
                    .HasColumnName("GID");
                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");
                entity.Property(e => e.Feedback1)
                    .HasMaxLength(1000)
                    .HasColumnName("feedback");
                entity.Property(e => e.Rate).HasColumnName("rate");
                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.GidNavigation).WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.Gid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Game");

                entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Profile");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Gid);

                entity.ToTable("Game");

                entity.Property(e => e.Gid)
                    .HasMaxLength(50)
                    .HasColumnName("GID");
                entity.Property(e => e.Configuration).HasColumnName("configuration");
                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.DownloadFile).HasColumnName("downloadFile");
                entity.Property(e => e.GameName)
                    .HasMaxLength(50)
                    .HasColumnName("gameName");
                entity.Property(e => e.Picture).HasColumnName("picture");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.SellerName)
                    .HasMaxLength(50)
                    .HasColumnName("sellerName");
                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("status");
                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasMany(d => d.Cids).WithMany(p => p.Gids)
                    .UsingEntity<Dictionary<string, object>>(
                        "CategoryConnect",
                        r => r.HasOne<Category>().WithMany()
                            .HasForeignKey("Cid")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_CategoryConnect_Category"),
                        l => l.HasOne<Game>().WithMany()
                            .HasForeignKey("Gid")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_CategoryConnect_Game"),
                        j =>
                        {
                            j.HasKey("Gid", "Cid");
                            j.ToTable("CategoryConnect");
                            j.IndexerProperty<string>("Gid")
                                .HasMaxLength(50)
                                .HasColumnName("GID");
                            j.IndexerProperty<string>("Cid")
                                .HasMaxLength(50)
                                .HasColumnName("CID");
                        });

                entity.HasMany(d => d.UsernamesNavigation).WithMany(p => p.GidsNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "Wishlist",
                        r => r.HasOne<Profile>().WithMany()
                            .HasForeignKey("Username")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_Wishlist_Profile"),
                        l => l.HasOne<Game>().WithMany()
                            .HasForeignKey("Gid")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_Wishlist_Game"),
                        j =>
                        {
                            j.HasKey("Gid", "Username");
                            j.ToTable("Wishlist");
                            j.IndexerProperty<string>("Gid")
                                .HasMaxLength(50)
                                .HasColumnName("GID");
                            j.IndexerProperty<string>("Username")
                                .HasMaxLength(50)
                                .HasColumnName("username");
                        });
            });

            modelBuilder.Entity<MoneyHistory>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("MoneyHistory");

                entity.Property(e => e.Mid)
                    .HasMaxLength(50)
                    .HasColumnName("MID");
                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");
                entity.Property(e => e.Money).HasColumnName("money");
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.MoneyHistories)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoneyHistory_Profile");
            });

            modelBuilder.Entity<MoneyManagement>(entity =>
            {
                entity.HasKey(e => new { e.Odid, e.Username });

                entity.ToTable("MoneyManagement");

                entity.Property(e => e.Odid)
                    .HasMaxLength(50)
                    .HasColumnName("ODID");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
                entity.Property(e => e.Admin)
                    .HasMaxLength(50)
                    .HasColumnName("admin");
                entity.Property(e => e.Admoney).HasColumnName("admoney");
                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");
                entity.Property(e => e.SellerMoney).HasColumnName("sellerMoney");

                entity.HasOne(d => d.Od).WithMany(p => p.MoneyManagements)
                    .HasForeignKey(d => d.Odid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoneyManagement_OrderDetail");

                entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.MoneyManagements)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoneyManagement_Profile");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Oid);

                entity.ToTable("Order");

                entity.Property(e => e.Oid)
                    .HasMaxLength(50)
                    .HasColumnName("OID");
                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");
                entity.Property(e => e.Total).HasColumnName("total");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Profile");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.Odid).HasName("PK_OrderDetail_1");

                entity.ToTable("OrderDetail");

                entity.Property(e => e.Odid)
                    .HasMaxLength(50)
                    .HasColumnName("ODID");
                entity.Property(e => e.Gid)
                    .HasMaxLength(50)
                    .HasColumnName("GID");
                entity.Property(e => e.Oid)
                    .HasMaxLength(50)
                    .HasColumnName("OID");
                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.GidNavigation).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.Gid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Game");

                entity.HasOne(d => d.OidNavigation).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.Oid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("Profile");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");
                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");
                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .HasColumnName("fullname");
                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .HasColumnName("gender");
                entity.Property(e => e.Money).HasColumnName("money");
                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("status");
                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .HasColumnName("token");
                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasMany(d => d.Gids).WithMany(p => p.Usernames)
                    .UsingEntity<Dictionary<string, object>>(
                        "ShoppingCart",
                        r => r.HasOne<Game>().WithMany()
                            .HasForeignKey("Gid")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_ShoppingCart_Game"),
                        l => l.HasOne<Profile>().WithMany()
                            .HasForeignKey("Username")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_ShoppingCart_Profile"),
                        j =>
                        {
                            j.HasKey("Username", "Gid");
                            j.ToTable("ShoppingCart");
                            j.IndexerProperty<string>("Username")
                                .HasMaxLength(50)
                                .HasColumnName("username");
                            j.IndexerProperty<string>("Gid")
                                .HasMaxLength(50)
                                .HasColumnName("GID");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
