using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class THealthExamController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: THealthExam
        public ActionResult Index()
        {
            return View();
        }

        // GET: Details
        public ActionResult Details(int visitServiceId)
        {
            int visitId = db.TVisitServices.Where(x => x.intVisitServiceID == visitServiceId).Select(z => z.intVisitID).FirstOrDefault();
            Session["intVisitId"] = visitId;

            THealthExam healthExam = db.THealthExams.Where(x => x.intVisitServiceID == visitServiceId).FirstOrDefault();
            TEyeStatusInfo eyeStatusInfo = db.TEyeStatusInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TEarStatusInfo earStatusInfo = db.TEarStatusInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TSkinInfo skinInfo = db.TSkinInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TMouthInfo mouthInfo = db.TMouthInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TNoseThroatInfo noseThroatInfo = db.TNoseThroatInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TGIInfo gIInfo = db.TGIInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TNeurologicalInfo neurologicalInfo = db.TNeurologicalInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TAbdomenInfo abdomenInfo = db.TAbdomenInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TUrogenitalInfo urogenitalInfo = db.TUrogenitalInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TMusculoskeletalInfo musculoskeletalInfo = db.TMusculoskeletalInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TLungInfo lungInfo = db.TLungInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            THeartInfo heartInfo = db.THeartInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();

            HealthExam hExam = new HealthExam()
            {
                dblWeight = (float)healthExam.dblWeight,
                dblTemperature = (float)healthExam.dblTemperature,
                intHeartRate = healthExam.intHeartRate,
                intRespRate = healthExam.intRespRate,
                intCapillaryRefillTime = healthExam.intCapillaryRefillTime,
                strMucousMembrane = healthExam.strMucousMembrane,
                strNotes = healthExam.strNotes,

                isEyeNormal = eyeStatusInfo.isNormal,
                isDischarge = eyeStatusInfo.isDischarge,
                isInfection = eyeStatusInfo.isInfection,
                isSclerosisLeft = eyeStatusInfo.isSclerosisLeft,
                isSclerosisRight = eyeStatusInfo.isSclerosisRight,
                isCataractLeft = eyeStatusInfo.isCataractLeft,
                isCataractRight = eyeStatusInfo.isCataractRight,
                isEyeInflamed = eyeStatusInfo.isInflamed,
                isEyelidTumor = eyeStatusInfo.isEyelidTumor,

                isEarNormal = earStatusInfo.isNormal,
                isEarInflamed = earStatusInfo.isInflamed,
                isEarTumor = earStatusInfo.isTumor,
                isDirty = earStatusInfo.isDirty,
                isEarPainful = earStatusInfo.isPainful,
                isExcessiveHair = earStatusInfo.isExcessiveHair,

                isSkinNormal = skinInfo.isNormal,
                isScaly = skinInfo.isScaly,
                isInfected = skinInfo.isInfected,
                isMatted = skinInfo.isMatted,
                isSkinScrape = skinInfo.isSkinScrape,
                isPruritus = skinInfo.isPruritus,
                isHairLoss = skinInfo.isHairLoss,
                isMass = skinInfo.isMass,
                isSkinParasites = skinInfo.isParasites,

                isMouthNormal = mouthInfo.isNormal,
                isMouthTumor = mouthInfo.isTumor,
                isGingivitis = mouthInfo.isGingivitis,
                isPeriodontitis = mouthInfo.isPeriodontitis,
                isTartarBuildup = mouthInfo.isTartarBuildup,
                isLooseTeeth = mouthInfo.isLooseTeeth,
                isBiteOVerUnder = mouthInfo.isBiteOVerUnder,

                isNoseThroatNormal = noseThroatInfo.isNormal,
                isLargeLymphNodes = noseThroatInfo.isLargeLymphNodes,
                isInflamedThroat = noseThroatInfo.isInflamedThroat,
                isNasalDishcharge = noseThroatInfo.isNasalDishcharge,
                isInflamedTonsils = noseThroatInfo.isInflamedTonsils,

                isGINormal = gIInfo.isNormal,
                isExcessiveGas = gIInfo.isExcessiveGas,
                isGIParasites = gIInfo.isParasites,
                isAbnormalFeces = gIInfo.isAbnormalFeces,
                isAnorexia = gIInfo.isAnorexia,

                isNeurologicalNormal = neurologicalInfo.isNormal,
                isPLRL = neurologicalInfo.isPLRL,
                isPLRR = neurologicalInfo.isPLRR,
                isCPLF = neurologicalInfo.isCPLF,
                isCPRF = neurologicalInfo.isCPRF,
                isCPLR = neurologicalInfo.isCPLR,
                isCPRR = neurologicalInfo.isCPRR,
                isPalpebralL = neurologicalInfo.isPalpebralL,
                isPalpebralR = neurologicalInfo.isPalpebralR,

                isAbdomenNormal = abdomenInfo.isNormal,
                isAbnormalMass = abdomenInfo.isAbnormalMass,
                isAbdomenPainful = abdomenInfo.isPainful,
                isBloated = abdomenInfo.isBloated,
                isEnlarged = abdomenInfo.isEnlarged,
                isFluid = abdomenInfo.isFluid,
                isHernia = abdomenInfo.isHernia,

                isUrogenitalNormal = urogenitalInfo.isNormal,
                isUrogenAbnormalUrination = urogenitalInfo.isAbnormalUrination,
                isGenitalDischarge = urogenitalInfo.isGenitalDischarge,
                isAnalSacs = urogenitalInfo.isAnalSacs,
                isRectal = urogenitalInfo.isRectal,
                isMammaryTumors = urogenitalInfo.isMammaryTumors,
                isAbnormalTesticles = urogenitalInfo.isAbnormalTesticles,
                isBloodSeen = urogenitalInfo.isBloodSeen,

                isMusculoskeletalNormal = musculoskeletalInfo.isNormal,
                isJointProblems = musculoskeletalInfo.isJointProblems,
                isNailProblems = musculoskeletalInfo.isNailProblems,
                isLamenessLF = musculoskeletalInfo.isLamenessLF,
                isLamenessRF = musculoskeletalInfo.isLamenessRF,
                isLamenessLR = musculoskeletalInfo.isLamenessLR,
                isLamenessRR = musculoskeletalInfo.isLamenessRR,
                isLigaments = musculoskeletalInfo.isLigaments,

                isLungNormal = lungInfo.isNormal,
                isBreathingDifficulty = lungInfo.isBreathingDifficulty,
                isRapidRespiration = lungInfo.isRapidRespiration,
                isTrachealPinchPositive = lungInfo.isTrachealPinchPositive,
                isTrachealPinchNegative = lungInfo.isTrachealPinchNegative,
                isCongestion = lungInfo.isCongestion,
                isAbnormalSound = lungInfo.isAbnormalSound,

                isHeartNormal = heartInfo.isNormal,
                isMurMur = heartInfo.isMurMur,
                isFast = heartInfo.isFast,
                isSlow = heartInfo.isSlow,
                isMuffled = heartInfo.isMuffled

            };

            int id = (int)Session["intPetID"];
            ViewBag.Name = db.TPets.Where(x => x.intPetID == id).Select(z => z.strPetName).FirstOrDefault();
            return View(hExam);
        }

        // GET: Create 
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["intPetID"] = id;

            ViewBag.Name = db.TPets.Where(x => x.intPetID == id).Select(z => z.strPetName).FirstOrDefault();
            return View();
        }

        // GET: Create 
        [HttpPost]
        public ActionResult Create(HealthExam healthExam )
        {

            int healthExamService = db.TServices.Where(x => x.strServiceDesc == "Health Exam").Select(z => z.intServiceID).FirstOrDefault();
            int lastInsertedVisitID = (int)Session["intVisitId"];
            TVisitService visitService = new TVisitService()
            {
                intVisitID = lastInsertedVisitID,
                intServiceID = healthExamService
            };

            db.TVisitServices.Add(visitService);
            db.SaveChanges();
            int lastInsertedVisitServiceID = db.TVisitServices.Max(v => v.intVisitServiceID);

            SqlParameter[] param = new SqlParameter[]
            {
                //Health exam parameters 
                new SqlParameter("@dblWeight", healthExam.dblWeight),
                new SqlParameter("@dblTemperature", healthExam.dblTemperature),
                new SqlParameter("@intHeartRate", healthExam.intHeartRate),
                new SqlParameter("@intRespRate", healthExam.intRespRate),
                new SqlParameter("@intCapillaryRefillTime", healthExam.intCapillaryRefillTime),
                new SqlParameter("@strMucousMembrane", healthExam.strMucousMembrane),
                new SqlParameter("@intVisitServiceID", lastInsertedVisitServiceID),
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

            int lastInsertedHealthExamId = db.THealthExams.Max(v => v.intHealthExamID);
            Session["intHealthExamId"] = lastInsertedHealthExamId;

            return RedirectToAction("Index", "VisitServices");
        }

        public ActionResult Edit(int visitServiceId)
        {
            Session["intVisitServiceID"] = visitServiceId;

            THealthExam healthExam = db.THealthExams.Where(x => x.intVisitServiceID == visitServiceId).FirstOrDefault();
            TEyeStatusInfo eyeStatusInfo = db.TEyeStatusInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TEarStatusInfo earStatusInfo = db.TEarStatusInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TSkinInfo skinInfo = db.TSkinInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TMouthInfo mouthInfo = db.TMouthInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TNoseThroatInfo noseThroatInfo = db.TNoseThroatInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TGIInfo gIInfo = db.TGIInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TNeurologicalInfo neurologicalInfo = db.TNeurologicalInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TAbdomenInfo abdomenInfo = db.TAbdomenInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TUrogenitalInfo urogenitalInfo = db.TUrogenitalInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TMusculoskeletalInfo musculoskeletalInfo = db.TMusculoskeletalInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            TLungInfo lungInfo = db.TLungInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();
            THeartInfo heartInfo = db.THeartInfos.Where(x => x.intHealthExamID == healthExam.intHealthExamID).FirstOrDefault();

            HealthExam hExam = new HealthExam()
            {
                dblWeight = (float)healthExam.dblWeight,
                dblTemperature = (float)healthExam.dblTemperature,
                intHeartRate = healthExam.intHeartRate,
                intRespRate = healthExam.intRespRate,
                intCapillaryRefillTime = healthExam.intCapillaryRefillTime,
                strMucousMembrane = healthExam.strMucousMembrane,
                strNotes = healthExam.strNotes,

                isEyeNormal = eyeStatusInfo.isNormal,
                isDischarge = eyeStatusInfo.isDischarge,
                isInfection = eyeStatusInfo.isInfection,
                isSclerosisLeft = eyeStatusInfo.isSclerosisLeft,
                isSclerosisRight = eyeStatusInfo.isSclerosisRight,
                isCataractLeft = eyeStatusInfo.isCataractLeft,
                isCataractRight = eyeStatusInfo.isCataractRight,
                isEyeInflamed = eyeStatusInfo.isInflamed,
                isEyelidTumor = eyeStatusInfo.isEyelidTumor,

                isEarNormal = earStatusInfo.isNormal,
                isEarInflamed = earStatusInfo.isInflamed,
                isEarTumor = earStatusInfo.isTumor,
                isDirty = earStatusInfo.isDirty,
                isEarPainful = earStatusInfo.isPainful,
                isExcessiveHair = earStatusInfo.isExcessiveHair,

                isSkinNormal = skinInfo.isNormal,
                isScaly = skinInfo.isScaly,
                isInfected = skinInfo.isInfected,
                isMatted = skinInfo.isMatted,
                isSkinScrape = skinInfo.isSkinScrape,
                isPruritus = skinInfo.isPruritus,
                isHairLoss = skinInfo.isHairLoss,
                isMass = skinInfo.isMass,
                isSkinParasites = skinInfo.isParasites,

                isMouthNormal = mouthInfo.isNormal,
                isMouthTumor = mouthInfo.isTumor,
                isGingivitis = mouthInfo.isGingivitis,
                isPeriodontitis = mouthInfo.isPeriodontitis,
                isTartarBuildup = mouthInfo.isTartarBuildup,
                isLooseTeeth = mouthInfo.isLooseTeeth,
                isBiteOVerUnder = mouthInfo.isBiteOVerUnder,

                isNoseThroatNormal = noseThroatInfo.isNormal,
                isLargeLymphNodes = noseThroatInfo.isLargeLymphNodes,
                isInflamedThroat = noseThroatInfo.isInflamedThroat,
                isNasalDishcharge = noseThroatInfo.isNasalDishcharge,
                isInflamedTonsils = noseThroatInfo.isInflamedTonsils,

                isGINormal = gIInfo.isNormal,
                isExcessiveGas = gIInfo.isExcessiveGas,
                isGIParasites = gIInfo.isParasites,
                isAbnormalFeces = gIInfo.isAbnormalFeces,
                isAnorexia = gIInfo.isAnorexia,

                isNeurologicalNormal = neurologicalInfo.isNormal,
                isPLRL = neurologicalInfo.isPLRL,
                isPLRR = neurologicalInfo.isPLRR,
                isCPLF = neurologicalInfo.isCPLF,
                isCPRF = neurologicalInfo.isCPRF,
                isCPLR = neurologicalInfo.isCPLR,
                isCPRR = neurologicalInfo.isCPRR,
                isPalpebralL = neurologicalInfo.isPalpebralL,
                isPalpebralR = neurologicalInfo.isPalpebralR,

                isAbdomenNormal = abdomenInfo.isNormal,
                isAbnormalMass = abdomenInfo.isAbnormalMass,
                isAbdomenPainful = abdomenInfo.isPainful,
                isBloated = abdomenInfo.isBloated,
                isEnlarged = abdomenInfo.isEnlarged,
                isFluid = abdomenInfo.isFluid,
                isHernia = abdomenInfo.isHernia,

                isUrogenitalNormal = urogenitalInfo.isNormal,
                isUrogenAbnormalUrination = urogenitalInfo.isAbnormalUrination,
                isGenitalDischarge = urogenitalInfo.isGenitalDischarge,
                isAnalSacs = urogenitalInfo.isAnalSacs,
                isRectal = urogenitalInfo.isRectal,
                isMammaryTumors = urogenitalInfo.isMammaryTumors,
                isAbnormalTesticles = urogenitalInfo.isAbnormalTesticles,
                isBloodSeen = urogenitalInfo.isBloodSeen,

                isMusculoskeletalNormal = musculoskeletalInfo.isNormal,
                isJointProblems = musculoskeletalInfo.isJointProblems,
                isNailProblems = musculoskeletalInfo.isNailProblems,
                isLamenessLF = musculoskeletalInfo.isLamenessLF,
                isLamenessRF = musculoskeletalInfo.isLamenessRF,
                isLamenessLR = musculoskeletalInfo.isLamenessLR,
                isLamenessRR = musculoskeletalInfo.isLamenessRR,
                isLigaments = musculoskeletalInfo.isLigaments,

                isLungNormal = lungInfo.isNormal,
                isBreathingDifficulty = lungInfo.isBreathingDifficulty,
                isRapidRespiration = lungInfo.isRapidRespiration,
                isTrachealPinchPositive = lungInfo.isTrachealPinchPositive,
                isTrachealPinchNegative = lungInfo.isTrachealPinchNegative,
                isCongestion = lungInfo.isCongestion,
                isAbnormalSound = lungInfo.isAbnormalSound,

                isHeartNormal = heartInfo.isNormal,
                isMurMur = heartInfo.isMurMur,
                isFast = heartInfo.isFast,
                isSlow = heartInfo.isSlow,
                isMuffled = heartInfo.isMuffled

            };

            int id = (int)Session["intPetID"];
            ViewBag.Name = db.TPets.Where(x => x.intPetID == id).Select(z => z.strPetName).FirstOrDefault();
            return View(hExam);
        }

        [HttpPost]
        public ActionResult Edit(HealthExam healthExam)
        {
            int visitServiceId = (int)Session["intVisitServiceID"];
            int healthExamId = db.THealthExams.Where(x => x.intVisitServiceID == visitServiceId).Select(z => z.intHealthExamID).FirstOrDefault();

            //Health Exam
            THealthExam exam = new THealthExam()
            {
                intHealthExamID = healthExamId,
                dblWeight = (float)healthExam.dblWeight,
                dblTemperature = (float)healthExam.dblTemperature,
                intHeartRate = healthExam.intHeartRate,
                intRespRate = healthExam.intRespRate,
                intCapillaryRefillTime = healthExam.intCapillaryRefillTime,
                strMucousMembrane = healthExam.strMucousMembrane,
                intVisitServiceID = visitServiceId,
                strNotes = healthExam.strNotes
            };

            db.Entry(exam).State = EntityState.Modified;
            db.SaveChanges();
            
            //Eye Status Information
            int eyestatusId = db.TEyeStatusInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intEyeStatusInfoID).FirstOrDefault();
            TEyeStatusInfo eyeStatus = new TEyeStatusInfo()
            {
                intEyeStatusInfoID = eyestatusId,
                isNormal = healthExam.isEyeNormal,
                isDischarge = healthExam.isDischarge,
                isInfection = healthExam.isInfection,
                isSclerosisLeft = healthExam.isSclerosisLeft,
                isSclerosisRight = healthExam.isSclerosisRight,
                isCataractLeft = healthExam.isCataractLeft,
                isCataractRight = healthExam.isCataractRight,
                isInflamed = healthExam.isEyeInflamed,
                isEyelidTumor = healthExam.isEyelidTumor,
                intHealthExamID = healthExamId
            };

            db.Entry(eyeStatus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Ear Status Information
            int earstatusId = db.TEarStatusInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intEarStatusInfoID).FirstOrDefault();
            TEarStatusInfo earStatus = new TEarStatusInfo()
            {
                intEarStatusInfoID = earstatusId,
                isNormal = healthExam.isEarNormal,
                isInflamed = healthExam.isEarInflamed,
                isTumor = healthExam.isEarTumor,
                isDirty = healthExam.isDirty,
                isPainful = healthExam.isEarPainful,
                isExcessiveHair = healthExam.isExcessiveHair,
                intHealthExamID = healthExamId
            };

            db.Entry(earStatus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Skin Status Information
            int skinstatusId = db.TSkinInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intSkinInfoID).FirstOrDefault();
            TSkinInfo skinInfo = new TSkinInfo()
            {
                intSkinInfoID = earstatusId,
                isNormal = healthExam.isSkinNormal,
                isScaly = healthExam.isScaly,
                isInfected = healthExam.isInfected,
                isMatted = healthExam.isMatted,
                isSkinScrape = healthExam.isSkinScrape,
                isPruritus = healthExam.isPruritus,
                isHairLoss = healthExam.isHairLoss,
                isMass = healthExam.isMass,
                isParasites = healthExam.isSkinParasites,
                intHealthExamID = healthExamId
            };

            db.Entry(skinInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Mouth Status Infomration
            int mouthstatusId = db.TMouthInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intMouthInfoID).FirstOrDefault();
            TMouthInfo mouthInfo = new TMouthInfo()
            {
                intMouthInfoID = mouthstatusId,
                isNormal = healthExam.isMouthNormal,
                isTumor = healthExam.isMouthTumor,
                isGingivitis = healthExam.isGingivitis,
                isPeriodontitis = healthExam.isPeriodontitis,
                isTartarBuildup = healthExam.isTartarBuildup,
                isLooseTeeth = healthExam.isLooseTeeth,
                isBiteOVerUnder = healthExam.isBiteOVerUnder,
                intHealthExamID = healthExamId
            };

            db.Entry(mouthInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Nose and Throat Status Information
            int nosethroatstatusId = db.TNoseThroatInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intNoseThroatInfoID).FirstOrDefault();
            TNoseThroatInfo noseThroatInfo = new TNoseThroatInfo()
            {
                intNoseThroatInfoID = nosethroatstatusId,
                isNormal = healthExam.isNoseThroatNormal,
                isLargeLymphNodes = healthExam.isLargeLymphNodes,
                isInflamedThroat = healthExam.isInflamedThroat,
                isNasalDishcharge = healthExam.isNasalDishcharge,
                isInflamedTonsils = healthExam.isInflamedTonsils,
                intHealthExamID = healthExamId
            };

            db.Entry(noseThroatInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //GI Status Information
            int gistatusId = db.TGIInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intGIInfoID).FirstOrDefault();
            TGIInfo gIInfo = new TGIInfo()
            {
                intGIInfoID = nosethroatstatusId,
                isNormal = healthExam.isGINormal,
                isExcessiveGas = healthExam.isExcessiveGas,
                isParasites = healthExam.isGIParasites,
                isAbnormalFeces = healthExam.isAbnormalFeces,
                isAnorexia = healthExam.isAnorexia,
                intHealthExamID = healthExamId
            };

            db.Entry(gIInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Nuerological Status Information
            int neurologicalStatusInfo = db.TNeurologicalInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intNeurologicalInfoID).FirstOrDefault();
            TNeurologicalInfo neurologicalInfo = new TNeurologicalInfo()
            {
                intNeurologicalInfoID = nosethroatstatusId,
                isNormal = healthExam.isNeurologicalNormal,
                isPLRL = healthExam.isPLRL,
                isPLRR = healthExam.isPLRR,
                isCPLF = healthExam.isCPLF,
                isCPRF = healthExam.isCPRF,
                isCPLR = healthExam.isCPLR,
                isCPRR = healthExam.isCPRR,
                isPalpebralL = healthExam.isPalpebralL,
                isPalpebralR = healthExam.isPalpebralR,
                intHealthExamID = healthExamId
            };

            db.Entry(neurologicalInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Abdomen Status Information
            int abdomenstatusId = db.TAbdomenInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intAbdomenInfoID).FirstOrDefault();
            TAbdomenInfo abdomenInfo = new TAbdomenInfo()
            {
                intAbdomenInfoID = abdomenstatusId,
                isNormal = healthExam.isAbdomenNormal,
                isAbnormalMass = healthExam.isAbnormalMass,
                isPainful = healthExam.isAbdomenPainful,
                isBloated = healthExam.isBloated,
                isEnlarged = healthExam.isEnlarged,
                isFluid = healthExam.isFluid,
                isHernia = healthExam.isHernia,
                intHealthExamID = healthExamId
            };

            db.Entry(abdomenInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Urogenital Status Information
            int urogenitalstatusId = db.TUrogenitalInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intUrogenitalInfoID).FirstOrDefault();
            TUrogenitalInfo urogenitalInfo = new TUrogenitalInfo()
            {
                intUrogenitalInfoID = urogenitalstatusId,
                isNormal = healthExam.isUrogenitalNormal,
                isAbnormalUrination = healthExam.isUrogenAbnormalUrination,
                isGenitalDischarge = healthExam.isGenitalDischarge,
                isAnalSacs = healthExam.isAnalSacs,
                isRectal = healthExam.isRectal,
                isMammaryTumors = healthExam.isMammaryTumors,
                isAbnormalTesticles = healthExam.isAbnormalTesticles,
                isBloodSeen = healthExam.isBloodSeen,
                intHealthExamID = healthExamId
            };

            db.Entry(urogenitalInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Musculoskeletal Status Information
            int musculoskeletalstatusId = db.TMusculoskeletalInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intMusculoskeletalInfoID).FirstOrDefault();
            TMusculoskeletalInfo musculoskeletalInfo = new TMusculoskeletalInfo()
            {
                intMusculoskeletalInfoID = musculoskeletalstatusId,
                isNormal = healthExam.isMusculoskeletalNormal,
                isJointProblems = healthExam.isJointProblems,
                isNailProblems = healthExam.isNailProblems,
                isLamenessLF = healthExam.isLamenessLF,
                isLamenessRF = healthExam.isLamenessRF,
                isLamenessLR = healthExam.isLamenessLR,
                isLamenessRR = healthExam.isLamenessRR,
                isLigaments = healthExam.isLigaments,
                intHealthExamID = healthExamId
            };

            db.Entry(musculoskeletalInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //Lung Status Information
            int lungstatusId = db.TLungInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intLungInfoID).FirstOrDefault();
            TLungInfo lungInfo = new TLungInfo()
            {
                intLungInfoID = lungstatusId,
                isNormal = healthExam.isLungNormal,
                isBreathingDifficulty = healthExam.isBreathingDifficulty,
                isRapidRespiration = healthExam.isRapidRespiration,
                isTrachealPinchPositive = healthExam.isTrachealPinchPositive,
                isTrachealPinchNegative = healthExam.isTrachealPinchNegative,
                isCongestion = healthExam.isCongestion,
                isAbnormalSound = healthExam.isAbnormalSound,
                intHealthExamID = healthExamId
            };

            db.Entry(lungInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            //Heart Status Information
            int heartstatusId = db.THeartInfos.Where(x => x.intHealthExamID == healthExamId).Select(z => z.intHeartInfoID).FirstOrDefault();
            THeartInfo heartInfo = new THeartInfo()
            {
                intHeartInfoID = heartstatusId,
                isNormal = healthExam.isHeartNormal,
                isMurMur = healthExam.isMurMur,
                isFast = healthExam.isFast,
                isSlow = healthExam.isSlow,
                isMuffled = healthExam.isMuffled,
                intHealthExamID = healthExamId
            };

            db.Entry(heartInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "VisitServices");
        }
    }
}