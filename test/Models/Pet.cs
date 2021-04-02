//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.IO;
//using System.ComponentModel.DataAnnotations;


//namespace test.Models
//{
//    public class Pet
//    {
//		public DateTime Start;
//		public DateTime End;
//		public TUser User;
//		public List<PetImage> PetImages;
//		public PetImage EventImage;
//		//private CapstoneEntities db = new CapstoneEntities();



//		public PetImage PrimaryImage {
//			get {
//				if (this.PetImages != null) {
//					foreach (PetImage i in this.PetImages) {
//						if (i.Primary) return i;
//					}
//				}
//				return new PetImage();
//			}
//		}

//		public bool Editable {
//			get {
//				if (this.Start == null) return true;
//				if (this.Start > DateTime.Now) return true;
//				return false;
//			}
//		}


//		public Pet GetPet(long ID) {
//			try {
//				CapstoneEntities db = new CapstoneEntities();
//				List<Pet> pets = new List<Pet>();
//				if (this.User == null) {
//					pets = db.GetPets(ID);
//				}
//				else {
//					pets = db.GetPets(ID, this.User.intUserID);
//				}
//				return pets[0];
//			}
//			catch (Exception ex) { throw new Exception(ex.Message); }
//		}

//		public sbyte AddPetImage(HttpPostedFileBase f) {
//			try {
//				this.EventImage = new PetImage();
//				this.EventImage.Primary = false;
//				this.EventImage.FileName = Path.GetFileName(f.FileName);

//				if (this.EventImage.IsImageFile()) {
//					this.EventImage.Size = f.ContentLength;
//					Stream stream = f.InputStream;
//					BinaryReader binaryReader = new BinaryReader(stream);
//					this.EventImage.ImageData = binaryReader.ReadBytes((int)stream.Length);
//					//this.UpdatePrimaryImage();
//				}
//				return 0;
//			}
//			catch (Exception ex) { throw new Exception(ex.Message); }

//		}

//		//public sbyte UpdatePrimaryImage() {
//		//	try {
//		//		CapstoneEntities db = new CapstoneEntities();
//		//		long NewID;
//		//		if (this.EventImage.PetImageID == 0) {
//		//			NewID = db.InsertPetImage(this);
//		//			if (NewID > 0) EventImage.PetImageID = NewID;
//		//		}
//		//		else {
//		//			db.UpdatePetImage(this);
//		//		}
//		//		return 0;
//		//	}
//		//	catch (Exception ex) { throw new Exception(ex.Message); }
//		//}


//	}
//}
