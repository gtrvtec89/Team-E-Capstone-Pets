namespace test.Models
{
    public class HealthExam
    {
        // Health Exam Parameters 
        public float dblWeight { get; set; }
        public float dblTemperature { get; set; }
        public int intHeartRate { get; set; }
        public int intRespRate { get; set; }
        public int intCapillaryRefillTime { get; set; }
        public string strMucousMembrane { get; set; }

        // TO DO: Pull this information from the session or something
        // public int intVisitServiceID { get; set; }

        public string strNotes { get; set; }

        // Eye Status Info Parameters
        public bool isEyeNormal { get; set; }
        public bool isDischarge { get; set; }
        public bool isInfection { get; set; }
        public bool isSclerosisLeft { get; set; }
        public bool isSclerosisRight { get; set; }
        public bool isCataractLeft { get; set; }
        public bool isCataractRight { get; set; }
        public bool isEyeInflamed { get; set; }
        public bool isEyelidTumor { get; set; }


        // Ear Status Info Parameters 
        public bool isEarNormal { get; set; }
        public bool isEarInflamed { get; set; }
        public bool isEarTumor { get; set; }
        public bool isDirty { get; set; }
        public bool isEarPainful { get; set; }
        public bool isExcessiveHair { get; set; }

        // Skin Status Info Parameters 
        public bool isSkinNormal { get; set; }
        public bool isScaly { get; set; }
        public bool isInfected { get; set; }
        public bool isMatted { get; set; }
        public bool isSkinScrape { get; set; }
        public bool isPruritus { get; set; }
        public bool isHairLoss { get; set; }
        public bool isMass { get; set; }
        public bool isSkinParasites { get; set; }

        // Mouth Status Info Parameters 
        public bool isMouthNormal { get; set; }
        public bool isMouthTumor { get; set; }
        public bool isGingivitis { get; set; }
        public bool isPeriodontitis { get; set; }
        public bool isTartarBuildup { get; set; }
        public bool isLooseTeeth { get; set; }
        public bool isBiteOVerUnder { get; set; }

        // Nose and Throat Status Info Parameters
        public bool isNoseThroatNormal { get; set; }
        public bool isLargeLymphNodes { get; set; }
        public bool isInflamedThroat { get; set; }
        public bool isNasalDishcharge { get; set; }
        public bool isInflamedTonsils { get; set; }

        // GI Status Info Parameters
        public bool isGINormal { get; set; }
        public bool isExcessiveGas { get; set; }
        public bool isGIParasites { get; set; }
        public bool isAbnormalFeces { get; set; }
        public bool isAnorexia { get; set; }

        // Neurological Status Info Parameters
        public bool isNeurologicalNormal { get; set; }
        public bool isPLRL { get; set; }
        public bool isPLRR { get; set; }
        public bool isCPLF { get; set; }
        public bool isCPRF { get; set; }
        public bool isCPLR { get; set; }
        public bool isCPRR { get; set; }
        public bool isPalpebralL { get; set; }
        public bool isPalpebralR { get; set; }

        // Abdomen Status Info Parameters 
        public bool isAbdomenNormal { get; set; }
        public bool isAbnormalMass { get; set; }
        public bool isAbdomenPainful { get; set; }
        public bool isBloated { get; set; }
        public bool isEnlarged { get; set; }
        public bool isFluid { get; set; }
        public bool isHernia { get; set; }

        // Urogenital Status Info Parameters
        public bool isUrogenitalNormal { get; set; }
        public bool isUrogenAbnormalUrination { get; set; }
        public bool isGenitalDischarge { get; set; }
        public bool isAnalSacs { get; set; }
        public bool isRectal { get; set; }
        public bool isMammaryTumors { get; set; }
        public bool isAbnormalTesticles { get; set; }
        public bool isBloodSeen { get; set; }

        // Musculoskeletal Status Info Parameters
        public bool isMusculoskeletalNormal { get; set; }
        public bool isJointProblems { get; set; }
        public bool isNailProblems { get; set; }
        public bool isLamenessLF { get; set; }
        public bool isLamenessRF { get; set; }
        public bool isLamenessLR { get; set; }
        public bool isLamenessRR { get; set; }
        public bool isLigaments { get; set; }

        // Lung Status Info Parameters
        public bool isLungNormal { get; set; }
        public bool isBreathingDifficulty { get; set; }
        public bool isRapidRespiration { get; set; }
        public bool isTrachealPinchPositive { get; set; }
        public bool isTrachealPinchNegative { get; set; }
        public bool isCongestion { get; set; }
        public bool isAbnormalSound { get; set; }

        // Heart Status Info Parameters 
        public bool isHeartNormal { get; set; }
        public bool isMurMur { get; set; }
        public bool isFast { get; set; }
        public bool isSlow { get; set; }
        public bool isMuffled { get; set; }

    }
}
