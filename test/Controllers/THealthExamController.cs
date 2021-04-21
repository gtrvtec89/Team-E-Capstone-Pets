using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers {
    public class THealthExamController : Controller {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: THealthExam
        public ActionResult Index() {
            return View();
        }

        // GET: Create 
        public ActionResult Create(int? id, DateTime dateOfVisit) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["intPetID"] = id;
            var petName = db.TPets.Where(x => x.intPetID == id).Select(x => x.strPetName).FirstOrDefault();
            var petID = db.TPets.Where(x => x.intPetID == id).Select(x => x.strPetNumber).FirstOrDefault();
            var ownerName = (from o in db.TOwners
                             join p in db.TPets
                             on o.intOwnerID equals p.intOwnerID
                             where p.intPetID == id
                             select new {
                                 firstName = o.strFirstName,
                                 lastName = o.strLastName
                             }).FirstOrDefault();

            if (petName == null) {
                return HttpNotFound();
            }

            ViewBag.Date = dateOfVisit;
            ViewBag.Owner = ownerName.firstName + " " + ownerName.lastName;
            ViewBag.Patient = petName;
            ViewBag.PatientID = petID;

            return View();
        }

        // GET: Create 
        [HttpPost]

        public ActionResult Create(HealthExam healthExam) {

            SqlParameter[] param = new SqlParameter[]
            {
                //Health exam parameters 
                new SqlParameter("@dblWeight", healthExam.dblWeight),
                new SqlParameter("@dblTemperature", healthExam.dblTemperature),
                new SqlParameter("@intHeartRate", healthExam.intHeartRate),
                new SqlParameter("@intRespRate", healthExam.intRespRate),
                new SqlParameter("@intCapillaryRefillTime", healthExam.intCapillaryRefillTime),
                new SqlParameter("@strMucousMembrane", healthExam.strMucousMembrane),
                new SqlParameter("@intVisitServiceID", Session["intVisitServiceID"]),
                new SqlParameter("@strNotes", healthExam.strNotes),

                //Eye status parameters 
                new SqlParameter("@isEyeNormal", SqlDbType.Bit) { Value = healthExam.isEyeNormal},
                new SqlParameter("@isDischarge", SqlDbType.Bit) { Value = healthExam.isDischarge },
                new SqlParameter("@isInfection", SqlDbType.Bit) { Value = healthExam.isInfection },
                new SqlParameter("@isSclerosisLeft", SqlDbType.Bit) { Value = healthExam.isSclerosisLeft },
                new SqlParameter("@isSclerosisRight", SqlDbType.Bit) { Value = healthExam.isSclerosisRight },
                new SqlParameter("@isCataractLeft", SqlDbType.Bit) { Value = healthExam.isCataractLeft },
                new SqlParameter("@isCataractRight", SqlDbType.Bit) { Value = healthExam.isCataractRight },
                new SqlParameter("@isEyeInflamed", SqlDbType.Bit) { Value = healthExam.isEyeInflamed },
                new SqlParameter("@isEyelidTumor", SqlDbType.Bit) { Value = healthExam.isEyelidTumor },

                //Ear status information
                new SqlParameter("@isEarNormal", SqlDbType.Bit) { Value = healthExam.isEarNormal },
                new SqlParameter("@isEarInflamed", SqlDbType.Bit) { Value = healthExam.isEarInflamed },
                new SqlParameter("@isEarTumor", SqlDbType.Bit) { Value = healthExam.isEarTumor },
                new SqlParameter("@isDirty", SqlDbType.Bit) { Value = healthExam.isDirty },
                new SqlParameter("@isEarPainful", SqlDbType.Bit) { Value = healthExam.isEarPainful },
                new SqlParameter("@isExcessiveHair", SqlDbType.Bit) { Value = healthExam.isExcessiveHair },

                //Skin status information
                new SqlParameter("@isSkinNormal", SqlDbType.Bit) { Value = healthExam.isSkinNormal },
                new SqlParameter("@isScaly", SqlDbType.Bit) { Value = healthExam.isScaly },
                new SqlParameter("@isInfected", SqlDbType.Bit) { Value = healthExam.isInfected },
                new SqlParameter("@isMatted", SqlDbType.Bit) { Value = healthExam.isMatted },
                new SqlParameter("@isSkinScrape", SqlDbType.Bit) { Value = healthExam.isSkinScrape },
                new SqlParameter("@isPruritus", SqlDbType.Bit) { Value = healthExam.isPruritus },
                new SqlParameter("@isHairLoss", SqlDbType.Bit) { Value = healthExam.isHairLoss },
                new SqlParameter("@isMass", SqlDbType.Bit) { Value = healthExam.isMass },
                new SqlParameter("@isSkinParasites", SqlDbType.Bit) { Value = healthExam.isSkinParasites },

                //Mouth status information
                new SqlParameter("@isMouthNormal", SqlDbType.Bit) { Value = healthExam.isMouthNormal },
                new SqlParameter("@isMouthTumor", SqlDbType.Bit) { Value = healthExam.isMouthTumor },
                new SqlParameter("@isGingivitis", SqlDbType.Bit) { Value = healthExam.isGingivitis },
                new SqlParameter("@isPeriodontitis", SqlDbType.Bit) { Value = healthExam.isPeriodontitis },
                new SqlParameter("@isTartarBuildup", SqlDbType.Bit) { Value = healthExam.isTartarBuildup },
                new SqlParameter("@isLooseTeeth", SqlDbType.Bit) { Value = healthExam.isLooseTeeth },
                new SqlParameter("@isBiteOVerUnder", SqlDbType.Bit) { Value = healthExam.isBiteOVerUnder },

                //Nose and Throat status information
                new SqlParameter("@isNoseThroatNormal", SqlDbType.Bit) { Value = healthExam.isNoseThroatNormal },
                new SqlParameter("@isLargeLymphNodes", SqlDbType.Bit) { Value = healthExam.isLargeLymphNodes },
                new SqlParameter("@isInflamedThroat", SqlDbType.Bit) { Value = healthExam.isInflamedThroat },
                new SqlParameter("@isNasalDishcharge", SqlDbType.Bit) { Value = healthExam.isNasalDishcharge },
                new SqlParameter("@isInflamedTonsils", SqlDbType.Bit) { Value = healthExam.isInflamedTonsils },

                //GI status information
                new SqlParameter("@isGINormal", SqlDbType.Bit) { Value = healthExam.isGINormal },
                new SqlParameter("@isExcessiveGas", SqlDbType.Bit) { Value = healthExam.isExcessiveGas },
                new SqlParameter("@isGIParasites", SqlDbType.Bit) { Value = healthExam.isGIParasites },
                new SqlParameter("@isAbnormalFeces", SqlDbType.Bit) { Value = healthExam.isAbnormalFeces },
                new SqlParameter("@isAnorexia", SqlDbType.Bit) { Value = healthExam.isAnorexia },

                //Neurological status information
                new SqlParameter("@isNeurologicalNormal", SqlDbType.Bit) { Value = healthExam.isNeurologicalNormal },
                new SqlParameter("@isPLRL", SqlDbType.Bit) { Value = healthExam.isPLRL },
                new SqlParameter("@isPLRR", SqlDbType.Bit) { Value = healthExam.isPLRR },
                new SqlParameter("@isCPLF", SqlDbType.Bit) { Value = healthExam.isCPLF },
                new SqlParameter("@isCPRF", SqlDbType.Bit) { Value = healthExam.isCPRF },
                new SqlParameter("@isCPLR", SqlDbType.Bit) { Value = healthExam.isCPLR },
                new SqlParameter("@isCPRR", SqlDbType.Bit) { Value = healthExam.isCPRR },
                new SqlParameter("@isPalpebralL", SqlDbType.Bit) { Value = healthExam.isPalpebralL },
                new SqlParameter("@isPalpebralR", SqlDbType.Bit) { Value = healthExam.isPalpebralR },

                //Abdomen status information
                new SqlParameter("@isAbdomenNormal", SqlDbType.Bit) { Value = healthExam.isAbdomenNormal },
                new SqlParameter("@isAbnormalMass", SqlDbType.Bit) { Value = healthExam.isAbnormalMass },
                new SqlParameter("@isAbdomenPainful", SqlDbType.Bit) { Value = healthExam.isAbdomenPainful },
                new SqlParameter("@isBloated", SqlDbType.Bit) { Value = healthExam.isBloated },
                new SqlParameter("@isEnlarged", SqlDbType.Bit) { Value = healthExam.isEnlarged },
                new SqlParameter("@isFluid", SqlDbType.Bit) { Value = healthExam.isFluid },
                new SqlParameter("@isHernia", SqlDbType.Bit) { Value = healthExam.isHernia },

                //Urogenital status information
                new SqlParameter("@isUrogenitalNormal", SqlDbType.Bit) { Value = healthExam.isUrogenitalNormal },
                new SqlParameter("@isUrogenAbnormalUrination", SqlDbType.Bit) { Value = healthExam.isUrogenAbnormalUrination },
                new SqlParameter("@isGenitalDischarge", SqlDbType.Bit) { Value = healthExam.isGenitalDischarge },
                new SqlParameter("@isAnalSacs", SqlDbType.Bit) { Value = healthExam.isAnalSacs },
                new SqlParameter("@isRectal", SqlDbType.Bit) { Value = healthExam.isRectal },
                new SqlParameter("@isMammaryTumors", SqlDbType.Bit) { Value = healthExam.isMammaryTumors },
                new SqlParameter("@isAbnormalTesticles", SqlDbType.Bit) { Value = healthExam.isAbnormalTesticles },
                new SqlParameter("@isBloodSeen", SqlDbType.Bit) { Value = healthExam.isBloodSeen },

                //Musculoskeletal status information
                new SqlParameter("@isMusculoskeletalNormal", SqlDbType.Bit) { Value = healthExam.isMusculoskeletalNormal },
                new SqlParameter("@isJointProblems", SqlDbType.Bit) { Value = healthExam.isJointProblems },
                new SqlParameter("@isNailProblems", SqlDbType.Bit) { Value = healthExam.isNailProblems },
                new SqlParameter("@isLamenessLF", SqlDbType.Bit) { Value = healthExam.isLamenessLF },
                new SqlParameter("@isLamenessRF", SqlDbType.Bit) { Value = healthExam.isLamenessRF },
                new SqlParameter("@isLamenessLR", SqlDbType.Bit) { Value = healthExam.isLamenessLR },
                new SqlParameter("@isLamenessRR", SqlDbType.Bit) { Value = healthExam.isLamenessRR },
                new SqlParameter("@isLigaments", SqlDbType.Bit) { Value = healthExam.isLigaments },

                //Lung status information
                new SqlParameter("@isLungNormal", SqlDbType.Bit) { Value = healthExam.isLungNormal },
                new SqlParameter("@isBreathingDifficulty", SqlDbType.Bit) { Value = healthExam.isBreathingDifficulty },
                new SqlParameter("@isRapidRespiration", SqlDbType.Bit) { Value = healthExam.isRapidRespiration },
                new SqlParameter("@isTrachealPinchPositive", SqlDbType.Bit) { Value = healthExam.isTrachealPinchPositive },
                new SqlParameter("@isTrachealPinchNegative", SqlDbType.Bit) { Value = healthExam.isTrachealPinchNegative },
                new SqlParameter("@isCongestion", SqlDbType.Bit) { Value = healthExam.isCongestion },
                new SqlParameter("@isAbnormalSound", SqlDbType.Bit) { Value = healthExam.isAbnormalSound },

                //Heart status information
                new SqlParameter("@isHeartNormal", SqlDbType.Bit) { Value = healthExam.isHeartNormal },
                new SqlParameter("@isMurMur", SqlDbType.Bit) { Value = healthExam.isMurMur },
                new SqlParameter("@isFast", SqlDbType.Bit) { Value = healthExam.isFast },
                new SqlParameter("@isSlow", SqlDbType.Bit) { Value = healthExam.isSlow },
                new SqlParameter("@isMuffled", SqlDbType.Bit) { Value = healthExam.isMuffled }
            };

            db.Database.ExecuteSqlCommand("uspAddPetVisit @dblWeight, @dblTemperature,@intHeartRate,@intRespRate,@intCapillaryRefillTime,@strMucousMembrane,@intVisitServiceID,@strNotes,@isEyeNormal, @isDischarge, @isInfection,@isSclerosisLeft, @isSclerosisRight, @isCataractLeft, @isCataractRight, @isEyeInflamed, @isEyelidTumor,@isEarNormal,@isEarInflamed,@isEarTumor,@isDirty,@isEarPainful,@isExcessiveHair,@isSkinNormal,@isScaly,@isInfected,@isMatted,@isSkinScrape,@isPruritus,@isHairLoss,@isMass,@isSkinParasites,@isMouthNormal,@isMouthTumor,@isGingivitis,@isPeriodontitis,@isTartarBuildup,@isLooseTeeth,@isBiteOVerUnder,@isNoseThroatNormal,@isLargeLymphNodes,@isInflamedThroat,@isNasalDishcharge,@isInflamedTonsils,@isGINormal,@isExcessiveGas,@isGIParasites,@isAbnormalFeces,@isAnorexia,@isNeurologicalNormal,@isPLRL,@isPLRR,@isCPLF,@isCPRF,@isCPLR,@isCPRR,@isPalpebralL,@isPalpebralR,@isAbdomenNormal, @isAbnormalMass,@isAbdomenPainful,@isBloated, @isEnlarged,@isFluid,@isHernia,@isUrogenitalNormal, @isUrogenAbnormalUrination, @isGenitalDischarge, @isAnalSacs, @isRectal, @isMammaryTumors, @isAbnormalTesticles, @isBloodSeen,@isMusculoskeletalNormal,@isJointProblems,@isNailProblems,@isLamenessLF,@isLamenessRF,@isLamenessLR,@isLamenessRR,@isLigaments,@isLungNormal, @isBreathingDifficulty, @isRapidRespiration,@isTrachealPinchPositive,@isTrachealPinchNegative,@isCongestion,@isAbnormalSound,@isHeartNormal,@isMurMur,@isFast,@isSlow,@isMuffled"
                , param
                );
            return RedirectToAction("Index", "VisitServices");
        }
    }
}