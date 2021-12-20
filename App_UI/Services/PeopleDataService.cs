using App_UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
namespace App_UI.Services
{
    public class PeopleDataService : IDataService<Person>
    {
        List<Person> data;
        public string Filename { get; set; }

        /// <summary>
        /// Implémentation d'un Singleton avec un type Lazy
        /// Le type Lazy permet de charger la classe seulement au premier
        /// appel d'Instance. Cela permet d'économiser de la mémoire
        /// Remarquez que le constructeur est privé.
        /// ATTENTION! Le Lazy Loading n'est pas matière à l'examen.
        /// </summary>
        private static readonly Lazy<PeopleDataService> lazy = new Lazy<PeopleDataService>(() => new PeopleDataService());
        public static PeopleDataService Instance { get => lazy.Value; }

        private PeopleDataService()
        {
            /// Seulement si c'est une liste fixe!
            /// Autres méthodes si les données viennent de fichier 
            /// ou de base de données

            populate();
        }

        private void populate()
        {
            data = new List<Person>
            {
                new Person { FirstName = "Ayanna", LastName = "Vargas", BirthDay = DateTime.Parse("1967-12-25"), City = "Rock Forest", Province = "QC",PostalCode = "J1N 1H6", Email = "purus.in@semvitae.edu", Mobile = "462-515-8993", Phone = "658-774-3123"},
                new Person { FirstName = "Whitney", LastName = "Parks", BirthDay = DateTime.Parse("1978-03-24"), City = "White Rock", Province = "BC",PostalCode = "V4B 4N3", Email = "consectetuer.euismod@adipiscingelit.net", Mobile = "319-112-3705", Phone = "750-521-7193"},
                new Person { FirstName = "Louis", LastName = "Watts", BirthDay = DateTime.Parse("1974-07-09"), City = "Hochelaga", Province = "QC",PostalCode = "H1W 4H0", Email = "at@gravidamolestie.ca", Mobile = "668-418-2708", Phone = "594-392-0304"},
                new Person { FirstName = "Pamela", LastName = "Knapp", BirthDay = DateTime.Parse("1985-03-13"), City = "Bonnyville", Province = "AB",PostalCode = "T9N 5T8", Email = "eget.dictum@Aliquamvulputate.ca", Mobile = "378-588-7523", Phone = "990-335-8738"},
                new Person { FirstName = "Clinton", LastName = "Gallagher", BirthDay = DateTime.Parse("1949-11-30"), City = "Sackville", Province = "NB",PostalCode = "E4L 3K2", Email = "eget@etpede.com", Mobile = "165-365-4117", Phone = "441-356-8605"},
                new Person { FirstName = "Amal", LastName = "Cross", BirthDay = DateTime.Parse("1988-06-29"), City = "Mississauga", Province = "ON",PostalCode = "L4T 5G2", Email = "non@idrisus.org", Mobile = "871-998-7893", Phone = "339-403-1347"},
                new Person { FirstName = "Vanna", LastName = "Hyde", BirthDay = DateTime.Parse("1967-08-26"), City = "Gananoque", Province = "ON",PostalCode = "K7G 1R7", Email = "nonummy@augueporttitor.edu", Mobile = "205-696-4498", Phone = "624-767-4994"},
                new Person { FirstName = "Madonna", LastName = "Navarro", BirthDay = DateTime.Parse("1971-07-17"), City = "Peace River", Province = "AB",PostalCode = "T8S 7R4", Email = "nascetur.ridiculus.mus@liberomauris.edu", Mobile = "944-855-4066", Phone = "819-218-3780"},
                new Person { FirstName = "Rina", LastName = "Decker", BirthDay = DateTime.Parse("1940-12-25"), City = "Delhi", Province = "ON",PostalCode = "N4B 9M2", Email = "neque.non.quam@risus.co.uk", Mobile = "759-388-6783", Phone = "819-218-3780"},
                new Person { FirstName = "Dustin", LastName = "Cole", BirthDay = DateTime.Parse("1990-01-18"), City = "Delson", Province = "QC",PostalCode = "J5B 7K0", Email = "felis@ante.org", Mobile = "122-178-1426", Phone = "310-470-5590"},
                new Person { FirstName = "Kellie", LastName = "Hanson", BirthDay = DateTime.Parse("1997-11-30"), City = "La Prairie", Province = "QC",PostalCode = "J5R 3Y9", Email = "id.ante@lacusEtiambibendum.org", Mobile = "257-875-8962", Phone = "501-312-8343"},
                new Person { FirstName = "Cain", LastName = "Booth", BirthDay = DateTime.Parse("1951-11-13"), City = "Stratford", Province = "PE",PostalCode = "C1B 3Y1", Email = "tempus.lorem.fringilla@loremtristiquealiquet.com", Mobile = "887-859-0693", Phone = "465-265-1251"},
                new Person { FirstName = "Todd", LastName = "Christian", BirthDay = DateTime.Parse("1940-12-21"), City = "Ste. Anne", Province = "MB",PostalCode = "R5H 8N3", Email = "eget@acsemut.net", Mobile = "643-162-1501", Phone = "154-936-9265"},
                new Person { FirstName = "Hashim", LastName = "Hodge", BirthDay = DateTime.Parse("1956-12-20"), City = "Port-Cartier", Province = "QC",PostalCode = "G5B 6H4", Email = "in.tempus.eu@pede.net", Mobile = "750-521-7193", Phone = "706-722-6762"},
                new Person { FirstName = "Leah", LastName = "Miller", BirthDay = DateTime.Parse("1983-07-11"), City = "Minto", Province = "NB",PostalCode = "E4B 6S4", Email = "imperdiet.non@Vivamussit.com", Mobile = "129-122-7168", Phone = "800-792-8380"},
                new Person { FirstName = "Kenneth", LastName = "Roberts", BirthDay = DateTime.Parse("2000-07-02"), City = "Région de Beauce", Province = "QC",PostalCode = "G0M 4S9", Email = "orci.quis@Vestibulum.co.uk", Mobile = "490-990-4981", Phone = "510-433-5623"},
                new Person { FirstName = "Carly", LastName = "Christensen", BirthDay = DateTime.Parse("1961-05-16"), City = "Winfield", Province = "BC",PostalCode = "V4V 3X1", Email = "euismod@sodales.net", Mobile = "390-471-7006", Phone = "175-412-9657"},
                new Person { FirstName = "Malik", LastName = "Compton", BirthDay = DateTime.Parse("1997-12-10"), City = "Bruce Peninsula", Province = "ON",PostalCode = "N0H 6P3", Email = "eu@lacuspedesagittis.com", Mobile = "887-859-0693", Phone = "793-974-6366"},
                new Person { FirstName = "Victoria", LastName = "King", BirthDay = DateTime.Parse("1963-11-26"), City = "Caledonia", Province = "ON",PostalCode = "N3W 0C5", Email = "cursus.a@parturientmontes.edu", Mobile = "819-218-3780", Phone = "819-218-3780"},
                new Person { FirstName = "James", LastName = "Simon", BirthDay = DateTime.Parse("1941-07-11"), City = "Black Lake", Province = "QC",PostalCode = "G6H 6N6", Email = "bibendum.ullamcorper@arcu.ca", Mobile = "328-524-0475", Phone = "122-230-8999"},
                new Person { FirstName = "Christopher", LastName = "Monroe", BirthDay = DateTime.Parse("1944-08-20"), City = "Middlesex", Province = "ON",PostalCode = "N0M 6X1", Email = "iaculis@utodiovel.ca", Mobile = "205-696-4498", Phone = "375-845-8374"},
                new Person { FirstName = "Bradley", LastName = "Ramsey", BirthDay = DateTime.Parse("1980-03-07"), City = "Whitecourt", Province = "AB",PostalCode = "T7S 7R0", Email = "mi@massaSuspendisseeleifend.edu", Mobile = "460-394-9393", Phone = "129-122-7168"},
                new Person { FirstName = "Yoshi", LastName = "West", BirthDay = DateTime.Parse("1968-09-26"), City = "Kingston", Province = "NB",PostalCode = "E5S 1Y0", Email = "sagittis.augue.eu@sedleoCras.ca", Mobile = "668-418-2708", Phone = "463-957-1912"},
                new Person { FirstName = "May", LastName = "Frye", BirthDay = DateTime.Parse("1989-11-21"), City = "Gagetown", Province = "NB",PostalCode = "E5M 7T6", Email = "porttitor.vulputate@atiaculisquis.edu", Mobile = "209-918-3349", Phone = "460-394-9393"},
                new Person { FirstName = "Madeline", LastName = "Mclaughlin", BirthDay = DateTime.Parse("1972-07-09"), City = "Caledon", Province = "ON",PostalCode = "L7C 6R5", Email = "hymenaeos.Mauris@malesuadaaugue.ca", Mobile = "178-916-5106", Phone = "460-394-9393"},
                new Person { FirstName = "Eaton", LastName = "Wilson", BirthDay = DateTime.Parse("1946-12-25"), City = "Manotick", Province = "ON",PostalCode = "K4M 0G2", Email = "malesuada@a.co.uk", Mobile = "684-528-9015", Phone = "349-514-9365"},
                new Person { FirstName = "Scott", LastName = "Simpson", BirthDay = DateTime.Parse("1956-03-12"), City = "Squamish", Province = "BC",PostalCode = "V8B 4V0", Email = "a.scelerisque@convallisconvallis.ca", Mobile = "643-162-1501", Phone = "463-957-1912"},
                new Person { FirstName = "Jenette", LastName = "Michael", BirthDay = DateTime.Parse("1997-11-17"), City = "Saint-Constant", Province = "QC",PostalCode = "J5A 6H2", Email = "non@nibhPhasellusnulla.co.uk", Mobile = "401-455-7531", Phone = "386-338-5818"},
                new Person { FirstName = "Idona", LastName = "Humphrey", BirthDay = DateTime.Parse("1975-08-01"), City = "Dunnville", Province = "ON",PostalCode = "N1A 4E6", Email = "non.leo@magna.co.uk", Mobile = "667-760-4209", Phone = "883-996-7263"},
                new Person { FirstName = "Erin", LastName = "Mcguire", BirthDay = DateTime.Parse("1989-04-05"), City = "Marion Bridge", Province = "NS",PostalCode = "B1K 6B2", Email = "lorem.sit.amet@nisiAeneaneget.co.uk", Mobile = "532-155-0355", Phone = "279-805-8091"},
                new Person { FirstName = "Cooper", LastName = "Mejia", BirthDay = DateTime.Parse("1983-07-27"), City = "Port Perry", Province = "ON",PostalCode = "L9L 8H0", Email = "dui@dolor.ca", Mobile = "840-498-1759", Phone = "125-541-6727"},
                new Person { FirstName = "Skyler", LastName = "Whitaker", BirthDay = DateTime.Parse("1941-07-21"), City = "Lloydminster", Province = "SK",PostalCode = "S9V 4V0", Email = "felis@liberoIntegerin.ca", Mobile = "750-521-7193", Phone = "800-792-8380"},
                new Person { FirstName = "Anastasia", LastName = "Garza", BirthDay = DateTime.Parse("1982-03-30"), City = "Saint-Raymond", Province = "QC",PostalCode = "G3L 3T2", Email = "sit@gravida.org", Mobile = "776-290-4796", Phone = "310-470-5590"},
                new Person { FirstName = "Tana", LastName = "Walton", BirthDay = DateTime.Parse("1951-06-03"), City = "Oxford", Province = "ON",PostalCode = "N0J 6T8", Email = "risus@leo.co.uk", Mobile = "710-903-5222", Phone = "581-801-2829"},
                new Person { FirstName = "Tatum", LastName = "Montoya", BirthDay = DateTime.Parse("1942-12-07"), City = "Stanley", Province = "NB",PostalCode = "E6B 8C6", Email = "dolor@auctor.net", Mobile = "734-499-0531", Phone = "319-112-3705"},
                new Person { FirstName = "Tanek", LastName = "Jefferson", BirthDay = DateTime.Parse("1970-07-17"), City = "Fort Frances", Province = "ON",PostalCode = "P9A 7R6", Email = "Nullam@Nullam.co.uk", Mobile = "819-218-3780", Phone = "126-931-8145"},
                new Person { FirstName = "Zia", LastName = "Barber", BirthDay = DateTime.Parse("1960-11-16"), City = "Juan de Fuca Shore", Province = "BC",PostalCode = "V0S 2B5", Email = "dui.nec@quismassa.co.uk", Mobile = "624-767-4994", Phone = "605-414-3547"},
                new Person { FirstName = "Alvin", LastName = "Ferguson", BirthDay = DateTime.Parse("1981-08-02"), City = "Lower Skeena", Province = "BC",PostalCode = "V0V 3P9", Email = "lacus@elitCurabitur.org", Mobile = "643-162-1501", Phone = "571-332-7507"},
                new Person { FirstName = "Rhea", LastName = "Ewing", BirthDay = DateTime.Parse("1987-08-02"), City = "Région d'Oka", Province = "QC",PostalCode = "J0N 9C4", Email = "Quisque@neque.net", Mobile = "990-335-8738", Phone = "571-332-7507"},
                new Person { FirstName = "Jelani", LastName = "Sims", BirthDay = DateTime.Parse("1986-05-12"), City = "La Baie", Province = "QC",PostalCode = "G7B 9X2", Email = "feugiat@blanditmattisCras.co.uk", Mobile = "510-433-5623", Phone = "253-179-3847"},
                new Person { FirstName = "Clare", LastName = "Sanders", BirthDay = DateTime.Parse("1950-03-13"), City = "Downtown Toronto", Province = "ON",PostalCode = "M4W 2P7", Email = "semper.auctor@fermentumvel.edu", Mobile = "210-802-1203", Phone = "491-865-6707"},
                new Person { FirstName = "Cadman", LastName = "Howell", BirthDay = DateTime.Parse("1945-05-08"), City = "Senneville", Province = "QC",PostalCode = "H9K 1C9", Email = "natoque.penatibus@turpisNullaaliquet.co.uk", Mobile = "776-290-4796", Phone = "570-950-7912"},
                new Person { FirstName = "Camilla", LastName = "Warren", BirthDay = DateTime.Parse("1998-11-17"), City = "Sainte-Marie", Province = "QC",PostalCode = "G6E 6V8", Email = "turpis.non@Donec.org", Mobile = "726-132-7001", Phone = "257-875-8962"},
                new Person { FirstName = "Lester", LastName = "Castillo", BirthDay = DateTime.Parse("1944-04-19"), City = "Beauceville", Province = "QC",PostalCode = "G5X 0V1", Email = "nisi.a.odio@amet.co.uk", Mobile = "250-555-4617", Phone = "209-918-3349"},
                new Person { FirstName = "Denton", LastName = "Ashley", BirthDay = DateTime.Parse("1960-03-14"), City = "Hamilton", Province = "ON",PostalCode = "L8E 3J2", Email = "vel.turpis@at.co.uk", Mobile = "658-774-3123", Phone = "140-464-7242"},
                new Person { FirstName = "Kennan", LastName = "Hebert", BirthDay = DateTime.Parse("1942-11-12"), City = "Summerside", Province = "PE",PostalCode = "C1N 4P3", Email = "mi.Duis@dignissim.ca", Mobile = "726-132-7001", Phone = "465-265-1251"},
                new Person { FirstName = "Kelly", LastName = "Alexander", BirthDay = DateTime.Parse("1978-04-07"), City = "Petit-Rocher", Province = "NB",PostalCode = "E8J 6V8", Email = "lorem.fringilla@Inscelerisque.com", Mobile = "667-760-4209", Phone = "647-142-8014"},
                new Person { FirstName = "Felicia", LastName = "Wright", BirthDay = DateTime.Parse("1961-10-08"), City = "Lower Skeena", Province = "BC",PostalCode = "V0V 5J3", Email = "hymenaeos.Mauris.ut@liberomaurisaliquam.org", Mobile = "369-993-0222", Phone = "961-981-3934"},
                new Person { FirstName = "Fleur", LastName = "Luna", BirthDay = DateTime.Parse("1992-02-02"), City = "Cranbrook", Province = "BC",PostalCode = "V1C 7K0", Email = "urna.Vivamus@inlobortis.net", Mobile = "310-470-5590", Phone = "401-455-7531"},
                new Person { FirstName = "Patience", LastName = "Barnett", BirthDay = DateTime.Parse("1959-10-13"), City = "Brockville", Province = "ON",PostalCode = "K6V 0E3", Email = "sed.turpis.nec@sedpedeCum.net", Mobile = "658-774-3123", Phone = "615-661-9860"},
                new Person { FirstName = "Gil", LastName = "Jacobs", BirthDay = DateTime.Parse("1976-05-27"), City = "Highlands", Province = "BC",PostalCode = "V9B 5T9", Email = "Integer.vitae.nibh@accumsanlaoreetipsum.ca", Mobile = "362-728-0358", Phone = "693-607-7029"},
                new Person { FirstName = "Brennan", LastName = "Kent", BirthDay = DateTime.Parse("1950-04-11"), City = "Lockport", Province = "MB",PostalCode = "R1B 6X3", Email = "sem.Pellentesque@luctuslobortisClass.co.uk", Mobile = "501-312-8343", Phone = "570-950-7912"},
                new Person { FirstName = "Wang", LastName = "Patel", BirthDay = DateTime.Parse("1950-06-21"), City = "Stony Plain", Province = "AB",PostalCode = "T7Z 4C1", Email = "ridiculus.mus.Proin@velturpisAliquam.ca", Mobile = "734-499-0531", Phone = "477-162-3201"},
                new Person { FirstName = "Kendall", LastName = "Sosa", BirthDay = DateTime.Parse("1950-12-29"), City = "Thetford Mines", Province = "QC",PostalCode = "G6G 6B3", Email = "rutrum@egetvolutpatornare.com", Mobile = "441-356-8605", Phone = "827-654-9939"},
                new Person { FirstName = "Chava", LastName = "Contreras", BirthDay = DateTime.Parse("1966-03-10"), City = "Thompson", Province = "MB",PostalCode = "R8N 0N8", Email = "et.netus.et@ipsum.edu", Mobile = "668-418-2708", Phone = "571-332-7507"},
                new Person { FirstName = "Norman", LastName = "Lambert", BirthDay = DateTime.Parse("1977-07-25"), City = "St. Andrews", Province = "NB",PostalCode = "E5B 0G0", Email = "aliquam.enim@Integer.edu", Mobile = "122-230-8999", Phone = "465-265-1251"},
                new Person { FirstName = "Leroy", LastName = "Johnson", BirthDay = DateTime.Parse("1984-06-19"), City = "Kedgwick", Province = "NB",PostalCode = "E8B 8T0", Email = "Curabitur@Classaptenttaciti.co.uk", Mobile = "108-300-4964", Phone = "615-661-9860"},
                new Person { FirstName = "Holmes", LastName = "Goff", BirthDay = DateTime.Parse("1944-02-13"), City = "Coldbrook", Province = "NS",PostalCode = "B4R 5S0", Email = "dictum@tellus.org", Mobile = "210-802-1203", Phone = "254-344-8797"},
                new Person { FirstName = "Hayden", LastName = "Blackwell", BirthDay = DateTime.Parse("1973-04-14"), City = "Elgin", Province = "ON",PostalCode = "N0L 0H9", Email = "quis.diam@metus.edu", Mobile = "254-344-8797", Phone = "477-162-3201"},
                new Person { FirstName = "Reece", LastName = "Burton", BirthDay = DateTime.Parse("1955-12-16"), City = "Rockland", Province = "ON",PostalCode = "K4K 1Y8", Email = "dis.parturient@sociis.net", Mobile = "956-952-7149", Phone = "726-132-7001"},
                new Person { FirstName = "Ryder", LastName = "Franklin", BirthDay = DateTime.Parse("1994-12-23"), City = "Pintendre", Province = "QC",PostalCode = "G6C 6R9", Email = "bibendum.ullamcorper@dolorQuisquetincidunt.com", Mobile = "154-936-9265", Phone = "349-514-9365"},
                new Person { FirstName = "Noah", LastName = "Meyer", BirthDay = DateTime.Parse("1941-04-09"), City = "Nackawic", Province = "NB",PostalCode = "E6G 7K4", Email = "Sed.dictum@Crasegetnisi.org", Mobile = "581-801-2829", Phone = "430-753-9431"},
                new Person { FirstName = "Zorita", LastName = "Carlson", BirthDay = DateTime.Parse("1979-11-28"), City = "Orleans", Province = "ON",PostalCode = "K1E 6K0", Email = "diam.lorem.auctor@utmi.net", Mobile = "668-418-2708", Phone = "961-981-3934"},
                new Person { FirstName = "Leroy", LastName = "Haney", BirthDay = DateTime.Parse("1998-02-07"), City = "Peterborough", Province = "ON",PostalCode = "K9K 3J0", Email = "lectus.a@metussit.edu", Mobile = "759-388-6783", Phone = "125-541-6727"},
                new Person { FirstName = "Warren", LastName = "Page", BirthDay = DateTime.Parse("1984-04-24"), City = "Bonnyville", Province = "AB",PostalCode = "T9N 5H9", Email = "ligula.elit.pretium@necmaurisblandit.net", Mobile = "122-178-1426", Phone = "624-767-4994"},
                new Person { FirstName = "Zia", LastName = "Higgins", BirthDay = DateTime.Parse("1971-09-08"), City = "Mount Pearl", Province = "LB",PostalCode = "A1N 4Y6", Email = "Pellentesque.ultricies@PraesentluctusCurabitur.edu", Mobile = "602-274-7457", Phone = "647-142-8014"},
                new Person { FirstName = "Zachary", LastName = "Maxwell", BirthDay = DateTime.Parse("1982-05-09"), City = "Dominion", Province = "NS",PostalCode = "B1G 3H6", Email = "sit.amet@odiotristique.com", Mobile = "129-122-7168", Phone = "122-230-8999"},
                new Person { FirstName = "Brianna", LastName = "Oneill", BirthDay = DateTime.Parse("1943-07-25"), City = "Debec", Province = "NB",PostalCode = "E7N 8A4", Email = "rhoncus.Donec@Classaptenttaciti.com", Mobile = "710-903-5222", Phone = "165-365-4117"},
                new Person { FirstName = "Phyllis", LastName = "Mcgowan", BirthDay = DateTime.Parse("1981-06-20"), City = "Estrie-Est", Province = "QC",PostalCode = "J0B 7B3", Email = "nunc.Quisque@vehiculaPellentesquetincidunt.ca", Mobile = "378-588-7523", Phone = "125-541-6727"},
                new Person { FirstName = "Lucius", LastName = "Blevins", BirthDay = DateTime.Parse("1998-05-04"), City = "Îles-Laval", Province = "QC",PostalCode = "H7Y 8B4", Email = "egestas.ligula@pulvinararcuet.net", Mobile = "380-642-0319", Phone = "690-264-9170"},
                new Person { FirstName = "Chase", LastName = "Sears", BirthDay = DateTime.Parse("1943-10-25"), City = "Renfrew", Province = "ON",PostalCode = "K7V 9L7", Email = "felis.orci@consectetuer.ca", Mobile = "463-957-1912", Phone = "310-470-5590"},
                new Person { FirstName = "Flynn", LastName = "Gardner", BirthDay = DateTime.Parse("1944-11-04"), City = "Parry Sound", Province = "ON",PostalCode = "P2A 9G1", Email = "mauris.erat@euismodmauris.edu", Mobile = "477-162-3201", Phone = "571-332-7507"},
                new Person { FirstName = "Sebastian", LastName = "Byers", BirthDay = DateTime.Parse("1991-07-12"), City = "Corner Brook", Province = "LB",PostalCode = "A2H 3V3", Email = "et@parturientmontes.net", Mobile = "667-760-4209", Phone = "491-865-6707"},
                new Person { FirstName = "Dale", LastName = "Cantrell", BirthDay = DateTime.Parse("1972-06-23"), City = "Iona", Province = "NS",PostalCode = "B2C 7A4", Email = "nec.mauris.blandit@Ut.co.uk", Mobile = "108-300-4964", Phone = "532-155-0355"},
                new Person { FirstName = "Stacey", LastName = "Clements", BirthDay = DateTime.Parse("1970-01-19"), City = "St. Stephen", Province = "NB",PostalCode = "E3L 9S6", Email = "ut.nisi@atliberoMorbi.com", Mobile = "550-230-2146", Phone = "693-607-7029"},
                new Person { FirstName = "Amy", LastName = "Gay", BirthDay = DateTime.Parse("1983-10-25"), City = "Laterrière", Province = "QC",PostalCode = "G7N 5H4", Email = "Nulla.dignissim@pellentesque.com", Mobile = "319-112-3705", Phone = "467-635-9200"},
                new Person { FirstName = "Joel", LastName = "Rollins", BirthDay = DateTime.Parse("1985-02-11"), City = "Stanley", Province = "NB",PostalCode = "E6B 4Y3", Email = "dictum@Proinultrices.edu", Mobile = "605-414-3547", Phone = "819-218-3780"},
                new Person { FirstName = "Wilma", LastName = "Noel", BirthDay = DateTime.Parse("1993-08-28"), City = "Olds", Province = "AB",PostalCode = "T4H 9K6", Email = "Curae.Phasellus.ornare@quam.org", Mobile = "684-528-9015", Phone = "380-642-0319"},
                new Person { FirstName = "Quamar", LastName = "Bender", BirthDay = DateTime.Parse("1963-08-19"), City = "Smiths Falls", Province = "ON",PostalCode = "K7A 5T4", Email = "nec.ante@non.org", Mobile = "310-470-5590", Phone = "460-394-9393"},
                new Person { FirstName = "Kimberly", LastName = "Saunders", BirthDay = DateTime.Parse("2000-03-05"), City = "Enfield", Province = "NS",PostalCode = "B2T 9X7", Email = "mauris@mauris.net", Mobile = "734-499-0531", Phone = "462-515-8993"},
                new Person { FirstName = "Madeline", LastName = "Roman", BirthDay = DateTime.Parse("1973-11-19"), City = "Dorchester", Province = "NB",PostalCode = "E4K 8H5", Email = "non@milaciniamattis.net", Mobile = "375-845-8374", Phone = "343-151-5411"},
                new Person { FirstName = "Justina", LastName = "Brady", BirthDay = DateTime.Parse("1977-10-02"), City = "Elliot Lake", Province = "ON",PostalCode = "P5A 1N5", Email = "accumsan@blandit.net", Mobile = "624-767-4994", Phone = "257-875-8962"},
                new Person { FirstName = "Marsden", LastName = "Savage", BirthDay = DateTime.Parse("1948-02-14"), City = "Caraquet", Province = "NB",PostalCode = "E1W 6S2", Email = "massa.non@Nullasemper.com", Mobile = "349-514-9365", Phone = "477-162-3201"},
                new Person { FirstName = "Austin", LastName = "Weiss", BirthDay = DateTime.Parse("1994-05-09"), City = "Beresford", Province = "NB",PostalCode = "E8K 1X1", Email = "accumsan.convallis.ante@convallisconvallis.co.uk", Mobile = "401-455-7531", Phone = "465-265-1251"},
                new Person { FirstName = "Stephanie", LastName = "Alexander", BirthDay = DateTime.Parse("1975-09-05"), City = "Reserved", Province = "QC",PostalCode = "H0H 2T7", Email = "eu.nulla@gravida.ca", Mobile = "380-642-0319", Phone = "375-845-8374"},
                new Person { FirstName = "Pamela", LastName = "Wise", BirthDay = DateTime.Parse("1972-10-31"), City = "Dunnville", Province = "ON",PostalCode = "N1A 6S6", Email = "Nullam.suscipit.est@duiquis.ca", Mobile = "154-892-7595", Phone = "535-812-9710"},
                new Person { FirstName = "Austin", LastName = "Lawson", BirthDay = DateTime.Parse("1961-09-30"), City = "Oromocto", Province = "NB",PostalCode = "E2V 7H3", Email = "tincidunt.neque.vitae@sagittisDuisgravida.org", Mobile = "759-388-6783", Phone = "460-394-9393"},
                new Person { FirstName = "Erasmus", LastName = "Mendez", BirthDay = DateTime.Parse("1943-09-12"), City = "Lanaudière-Nord", Province = "QC",PostalCode = "J0K 7N3", Email = "magna.Cras.convallis@facilisisnon.com", Mobile = "477-162-3201", Phone = "532-155-0355"},
                new Person { FirstName = "Camilla", LastName = "Rodriquez", BirthDay = DateTime.Parse("1996-08-27"), City = "Oxford", Province = "ON",PostalCode = "N0J 4K8", Email = "sit@eutellus.edu", Mobile = "739-100-1520", Phone = "571-332-7507"},
                new Person { FirstName = "Orla", LastName = "Larson", BirthDay = DateTime.Parse("1951-02-19"), City = "Saint-Jacques", Province = "NB",PostalCode = "E7B 6S0", Email = "feugiat@lacusQuisque.edu", Mobile = "776-290-4796", Phone = "254-344-8797"},
                new Person { FirstName = "Theodore", LastName = "Green", BirthDay = DateTime.Parse("1949-08-11"), City = "Pomona", Province = "CA",PostalCode = "91766", Email = "et.malesuada@nonenim.org", Mobile = "254-344-8797", Phone = "739-100-1520"},
                new Person { FirstName = "Hollee", LastName = "Little", BirthDay = DateTime.Parse("1989-05-23"), City = "Bakersfield", Province = "CA",PostalCode = "93309", Email = "Praesent.luctus.Curabitur@elit.org", Mobile = "990-335-8738", Phone = "175-412-9657"},
                new Person { FirstName = "Elaine", LastName = "Gonzalez", BirthDay = DateTime.Parse("1965-10-28"), City = "San Francisco", Province = "CA",PostalCode = "94110", Email = "dolor.vitae@auguemalesuada.net", Mobile = "490-990-4981", Phone = "178-916-5106"},
                new Person { FirstName = "Winifred", LastName = "England", BirthDay = DateTime.Parse("1980-06-30"), City = "San Marcos", Province = "CA",PostalCode = "92069", Email = "et.pede@intempus.edu", Mobile = "108-300-4964", Phone = "253-179-3847"},
                new Person { FirstName = "Vaughan", LastName = "Summers", BirthDay = DateTime.Parse("1973-05-29"), City = "Livermore", Province = "CA",PostalCode = "94550", Email = "suscipit.nonummy.Fusce@necorciDonec.ca", Mobile = "319-112-3705", Phone = "250-555-4617"},
                new Person { FirstName = "Dawn", LastName = "Terry", BirthDay = DateTime.Parse("1959-05-28"), City = "San Jose", Province = "CA",PostalCode = "95123", Email = "tortor.at@Maecenasmi.ca", Mobile = "279-805-8091", Phone = "477-162-3201"},
                new Person { FirstName = "Martena", LastName = "Henderson", BirthDay = DateTime.Parse("1988-03-25"), City = "El Monte", Province = "CA",PostalCode = "91732", Email = "rhoncus.Proin@et.org", Mobile = "819-218-3780", Phone = "746-317-6694"},
                new Person { FirstName = "Sheila", LastName = "Nichols", BirthDay = DateTime.Parse("1973-12-08"), City = "Porterville", Province = "CA",PostalCode = "93257", Email = "diam.Sed@fringillaornare.com", Mobile = "684-528-9015", Phone = "465-265-1251"},
                new Person { FirstName = "Castor", LastName = "Ward", BirthDay = DateTime.Parse("1970-09-22"), City = "Santa Ana", Province = "CA",PostalCode = "92704", Email = "semper@ullamcorper.org", Mobile = "624-767-4994", Phone = "129-122-7168"},
                new Person { FirstName = "Randall", LastName = "Griffith", BirthDay = DateTime.Parse("1977-12-27"), City = "Bakersfield", Province = "CA",PostalCode = "93306", Email = "Donec@vitaeorciPhasellus.ca", Mobile = "279-805-8091", Phone = "693-607-7029"},
            };

        }

        /// <summary>
        /// Sert à remplir la collection avec la string reçu en paramètre
        /// </summary>
        /// <param name="allContent">Le contenu doit être compatible JSON</param>
        /// <returns>Le nombre d'enregistrement importée</returns>
        public int SetAllFromJson(string allContent)
        {
 
            return 0;
        }

        /// <summary>
        /// Sérialise les données de la collection en format JSon
        /// </summary>
        /// <returns>Une string en format json</returns>
        public string GetAllAsJson()
        {
            /// TODO 02b : Compléter la méthode pour convertir les donnéesd

            return System.Text.Json.JsonSerializer.Serialize(data);

        }

        public IEnumerable<Person> GetAll()
        {
            /// Seulement pour des fins de tests
            foreach (Person p in data)
            {
                yield return p;
            }
        }

        public int Insert(Person record)
        {
            int result;

            try
            {
                data.Add(record);
                result = data.Count;
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public bool UpdateOrInsert(Person record)
        {
            bool result;

            try
            {
                if (!data.Contains(record))
                    result = Insert(record) > 0;
                else
                {
                    /// La liste contient des références
                    /// donc les mises à jour sont automatiques
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public int Delete(Person record)
        {
            int result = 0;

            try
            {
                if (data.Contains(record))
                {
                    data.Remove(record);
                    result = data.Count;
                }
                else
                {
                    result = 0;
                }


            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(Filename))
                throw new NullReferenceException($"{nameof(Filename)} property is empty or null");

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(Filename))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, data);
                }
            }

            return false;
        }

        public int Update(Person value)
        {
            if (value == null) return 0;

            Person p = GetById(value.Id);

            if (p == null)
            {
                return Insert(value);
            }


            Type type = p.GetType();

            /// Cette boucle permet de parcourir les propriétés de l'objet
            /// et ensuite de copier ce qui n'est pas égale.
            foreach (PropertyInfo info in type.GetProperties(
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Instance |
                        BindingFlags.GetProperty))
            {
                if (!info.CanWrite) continue;

                var oValue = info.GetValue(p, null);
                var nValue = info.GetValue(value, null);

                if (oValue != nValue)
                {
                    info.SetValue(p, info.GetValue(value, null));
                }
            }

            return 1;
        }

        public Person GetById(int id)
        {
            return data.Find(p => p.Id == id);
        }
    }
}
