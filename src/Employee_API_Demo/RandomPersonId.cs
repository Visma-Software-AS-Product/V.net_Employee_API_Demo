﻿using System;

namespace HRM_API_Demo
{
    static class RandomPersonId
    {
        private static string[] _personnr = new string[]
        {
            "17086926636",
            "03097514428",
            "26128838766",
            "26056435459",
            "17040170731",
            "03026239590",
            "21078548026",
            "01096102808",
            "03125718271",
            "17047838304",
            "26110079639",
            "26128014933",
            "07087332386",
            "25109013997",
            "18037549319",
            "16027049013",
            "14059849957",
            "13017100763",
            "18117505061",
            "13048919175",
            "12018947496",
            "20065620074",
            "14029008095",
            "29089532394",
            "13046916314",
            "05027310321",
            "19079429166",
            "21106518928",
            "29078945964",
            "02037331395",
            "23077634789",
            "23085941771",
            "21050277191",
            "11075921545",
            "13127025198",
            "07078620212",
            "10109148917",
            "19070069523",
            "17128524444",
            "02019623796",
            "17029131457",
            "20067707851",
            "14065832721",
            "12037919541",
            "27049839125",
            "03068137666",
            "27070260579",
            "03129905297",
            "24078706437",
            "17060376867",
            "13106304070",
            "07049433934",
            "10049635228",
            "02125826558",
            "03039937823",
            "12038423000",
            "27088704776",
            "07037508483",
            "22125804806",
            "07038213606",
            "18059033618",
            "28029104435",
            "26120166106",
            "27076514831",
            "04049117022",
            "25040174160",
            "03089343223",
            "27037537700",
            "17125816541",
            "05067704071",
            "15017320537",
            "26055705738",
            "05035931009",
            "16108607487",
            "12068010916",
            "31010267210",
            "14037746186",
            "26087720399",
            "30106241321",
            "20037136051",
            "29085516963",
            "13027008938",
            "08038327136",
            "23056427066",
            "18128225486",
            "17079230987",
            "22127934014",
            "16128814667",
            "05049626497",
            "04109229267",
            "16078009555",
            "28087037381",
            "08017439112",
            "17099540397",
            "01109347880",
            "03066518956",
            "26038105999",
            "06129708652",
            "25117728059",
            "27019323418"
        };

        public static string GetRandomPersonId()
        {
            int nbr = new Random().Next(0, 99);

            return _personnr[nbr];
        }
    }
}
