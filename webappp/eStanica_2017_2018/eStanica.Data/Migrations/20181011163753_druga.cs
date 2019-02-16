using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autobusi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    vrstaBusa = table.Column<string>(nullable: true),
                    brojSjedistaBusa = table.Column<int>(nullable: false),
                    Proizvodjac = table.Column<string>(nullable: true),
                    slikaAutobusa = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autobusi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    datumRodjenja = table.Column<DateTime>(nullable: false),
                    Spol = table.Column<string>(nullable: false),
                    Telefon = table.Column<string>(nullable: true),
                    ZemljaPorijekla = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    BrojIndeksa = table.Column<string>(nullable: true),
                    slikaIndeksa = table.Column<byte[]>(nullable: true),
                    slikaPenzionerskeKartice = table.Column<byte[]>(nullable: true),
                    GodineIskustva = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Popusti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    vrstaPopusta = table.Column<string>(nullable: true),
                    Postotak = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Popusti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prevoznici",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    KontaktTelefon = table.Column<int>(nullable: false),
                    webStranica = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prevoznici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SredstvoPlacanja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SredstvoPlacanja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoviKarte",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviKarte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    DrzavaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gradovi_Drzave_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PosjedujeAutobuse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrevoznikId = table.Column<int>(nullable: false),
                    AutobusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosjedujeAutobuse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosjedujeAutobuse_Autobusi_AutobusId",
                        column: x => x.AutobusId,
                        principalTable: "Autobusi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PosjedujeAutobuse_Prevoznici_PrevoznikId",
                        column: x => x.PrevoznikId,
                        principalTable: "Prevoznici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kartice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojKartice = table.Column<string>(nullable: true),
                    datumIsteka = table.Column<DateTime>(nullable: false),
                    Banka = table.Column<string>(nullable: true),
                    vrstaKartice = table.Column<string>(nullable: true),
                    StanjeRacuna = table.Column<float>(nullable: false),
                    SredstvoPlacanjaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kartice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kartice_SredstvoPlacanja_SredstvoPlacanjaId",
                        column: x => x.SredstvoPlacanjaId,
                        principalTable: "SredstvoPlacanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojKarte = table.Column<string>(nullable: true),
                    datumPutovanja = table.Column<DateTime>(nullable: false),
                    brojSjedista = table.Column<string>(nullable: true),
                    Aktivna = table.Column<bool>(nullable: false),
                    TipKarteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Karte_TipoviKarte_TipKarteId",
                        column: x => x.TipKarteId,
                        principalTable: "TipoviKarte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikUloge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    datumLogiranja = table.Column<DateTime>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    UlogeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikUloge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikUloge_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisnikUloge_Uloge_UlogeId",
                        column: x => x.UlogeId,
                        principalTable: "Uloge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Linije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    TipLinije = table.Column<string>(nullable: true),
                    RedniBrojLinije = table.Column<string>(nullable: true),
                    PrevoznikId = table.Column<int>(nullable: false),
                    PolazakId = table.Column<int>(nullable: false),
                    DestinacijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Linije_Gradovi_DestinacijaId",
                        column: x => x.DestinacijaId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Linije_Gradovi_PolazakId",
                        column: x => x.PolazakId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Linije_Prevoznici_PrevoznikId",
                        column: x => x.PrevoznikId,
                        principalTable: "Prevoznici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stanice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    GradId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stanice_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transakcije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    datumKupovine = table.Column<DateTime>(nullable: false),
                    brojTransakcije = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    otkazano = table.Column<bool>(nullable: false),
                    KlijentiId = table.Column<int>(nullable: true),
                    KlijentId = table.Column<int>(nullable: false),
                    KarticaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transakcije_Kartice_KarticaId",
                        column: x => x.KarticaId,
                        principalTable: "Kartice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transakcije_Korisnici_KlijentiId",
                        column: x => x.KlijentiId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ocjena = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    Komentar = table.Column<string>(nullable: true),
                    KlijentiId = table.Column<int>(nullable: true),
                    KlijentId = table.Column<int>(nullable: true),
                    KartaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzije_Karte_KartaId",
                        column: x => x.KartaId,
                        principalTable: "Karte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzije_Korisnici_KlijentiId",
                        column: x => x.KlijentiId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KretanjeAutobusa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrajanjePuta = table.Column<float>(nullable: false),
                    LinijeId = table.Column<int>(nullable: false),
                    AutobusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KretanjeAutobusa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KretanjeAutobusa_Autobusi_AutobusId",
                        column: x => x.AutobusId,
                        principalTable: "Autobusi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KretanjeAutobusa_Linije_LinijeId",
                        column: x => x.LinijeId,
                        principalTable: "Linije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PosjecujeLokacije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cijenaOdPolaska = table.Column<float>(nullable: false),
                    LinijeId = table.Column<int>(nullable: false),
                    GradId = table.Column<int>(nullable: false),
                    KartaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosjecujeLokacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosjecujeLokacije_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PosjecujeLokacije_Karte_KartaId",
                        column: x => x.KartaId,
                        principalTable: "Karte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PosjecujeLokacije_Linije_LinijeId",
                        column: x => x.LinijeId,
                        principalTable: "Linije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaobracajniDnevnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    vrijemeDolaska = table.Column<DateTime>(nullable: false),
                    vrijemeOdlaska = table.Column<DateTime>(nullable: false),
                    brojPutnogNaloga = table.Column<string>(nullable: true),
                    Zakasnjenje = table.Column<string>(nullable: true),
                    Primjedbe = table.Column<string>(nullable: true),
                    UposlenikId = table.Column<int>(nullable: false),
                    StanicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaobracajniDnevnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaobracajniDnevnik_Stanice_StanicaId",
                        column: x => x.StanicaId,
                        principalTable: "Stanice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaobracajniDnevnik_Korisnici_UposlenikId",
                        column: x => x.UposlenikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransakcijaStavke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Kolicina = table.Column<int>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false),
                    TransakcijaId = table.Column<int>(nullable: false),
                    KartaId = table.Column<int>(nullable: false),
                    PopustId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransakcijaStavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransakcijaStavke_Karte_KartaId",
                        column: x => x.KartaId,
                        principalTable: "Karte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransakcijaStavke_Popusti_PopustId",
                        column: x => x.PopustId,
                        principalTable: "Popusti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransakcijaStavke_Transakcije_TransakcijaId",
                        column: x => x.TransakcijaId,
                        principalTable: "Transakcije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gradovi_DrzavaId",
                table: "Gradovi",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_TipKarteId",
                table: "Karte",
                column: "TipKarteId");

            migrationBuilder.CreateIndex(
                name: "IX_Kartice_SredstvoPlacanjaId",
                table: "Kartice",
                column: "SredstvoPlacanjaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUloge_KorisnikId",
                table: "KorisnikUloge",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUloge_UlogeId",
                table: "KorisnikUloge",
                column: "UlogeId");

            migrationBuilder.CreateIndex(
                name: "IX_KretanjeAutobusa_AutobusId",
                table: "KretanjeAutobusa",
                column: "AutobusId");

            migrationBuilder.CreateIndex(
                name: "IX_KretanjeAutobusa_LinijeId",
                table: "KretanjeAutobusa",
                column: "LinijeId");

            migrationBuilder.CreateIndex(
                name: "IX_Linije_DestinacijaId",
                table: "Linije",
                column: "DestinacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Linije_PolazakId",
                table: "Linije",
                column: "PolazakId");

            migrationBuilder.CreateIndex(
                name: "IX_Linije_PrevoznikId",
                table: "Linije",
                column: "PrevoznikId");

            migrationBuilder.CreateIndex(
                name: "IX_PosjecujeLokacije_GradId",
                table: "PosjecujeLokacije",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_PosjecujeLokacije_KartaId",
                table: "PosjecujeLokacije",
                column: "KartaId");

            migrationBuilder.CreateIndex(
                name: "IX_PosjecujeLokacije_LinijeId",
                table: "PosjecujeLokacije",
                column: "LinijeId");

            migrationBuilder.CreateIndex(
                name: "IX_PosjedujeAutobuse_AutobusId",
                table: "PosjedujeAutobuse",
                column: "AutobusId");

            migrationBuilder.CreateIndex(
                name: "IX_PosjedujeAutobuse_PrevoznikId",
                table: "PosjedujeAutobuse",
                column: "PrevoznikId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_KartaId",
                table: "Recenzije",
                column: "KartaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_KlijentiId",
                table: "Recenzije",
                column: "KlijentiId");

            migrationBuilder.CreateIndex(
                name: "IX_SaobracajniDnevnik_StanicaId",
                table: "SaobracajniDnevnik",
                column: "StanicaId");

            migrationBuilder.CreateIndex(
                name: "IX_SaobracajniDnevnik_UposlenikId",
                table: "SaobracajniDnevnik",
                column: "UposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Stanice_GradId",
                table: "Stanice",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijaStavke_KartaId",
                table: "TransakcijaStavke",
                column: "KartaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijaStavke_PopustId",
                table: "TransakcijaStavke",
                column: "PopustId");

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijaStavke_TransakcijaId",
                table: "TransakcijaStavke",
                column: "TransakcijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_KarticaId",
                table: "Transakcije",
                column: "KarticaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_KlijentiId",
                table: "Transakcije",
                column: "KlijentiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikUloge");

            migrationBuilder.DropTable(
                name: "KretanjeAutobusa");

            migrationBuilder.DropTable(
                name: "PosjecujeLokacije");

            migrationBuilder.DropTable(
                name: "PosjedujeAutobuse");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "SaobracajniDnevnik");

            migrationBuilder.DropTable(
                name: "TransakcijaStavke");

            migrationBuilder.DropTable(
                name: "Uloge");

            migrationBuilder.DropTable(
                name: "Linije");

            migrationBuilder.DropTable(
                name: "Autobusi");

            migrationBuilder.DropTable(
                name: "Stanice");

            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "Popusti");

            migrationBuilder.DropTable(
                name: "Transakcije");

            migrationBuilder.DropTable(
                name: "Prevoznici");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "TipoviKarte");

            migrationBuilder.DropTable(
                name: "Kartice");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Drzave");

            migrationBuilder.DropTable(
                name: "SredstvoPlacanja");
        }
    }
}
