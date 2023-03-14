using System;
using System.Collections.Generic;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.API;
using Rage;

namespace YobbinCallouts
{
	
	internal class Citizen : Ped
	{
		
		
		
		public string FullName { get; private set; }

		
		
		
		public string TimesStopped { get; private set; }

		
		
		
		public WantedInformation WantedInformation { get; private set; }

		
		
		
		public string Forename { get; private set; }

		
		
		
		public bool Wanted { get; private set; }

		
		
		
		public List<string> MedicalProblems { get; private set; }

		
		
		
		public string Gender { get; private set; }

		
		public Citizen()
		{
			this.pedPersona = Functions.GetPersonaForPed(this);
			this.FullName = this.pedPersona.FullName;
			this.Forename = this.pedPersona.Forename;
			this.TimesStopped = this.pedPersona.TimesStopped.ToString();
			this.Wanted = this.pedPersona.Wanted;
			this.WantedInformation = this.pedPersona.WantedInformation;
			this.Gender = this.pedPersona.Gender.ToString();
			this.MedicalProblems = new List<string>();
		}

		
		public Citizen(Model modelName, Vector3 spawnPoint) : base(spawnPoint, modelName)
		{
			this.pedPersona = Functions.GetPersonaForPed(this);
			this.FullName = this.pedPersona.FullName;
			this.Forename = this.pedPersona.Forename;
			this.TimesStopped = this.pedPersona.TimesStopped.ToString();
			this.Wanted = this.pedPersona.Wanted;
			this.WantedInformation = this.pedPersona.WantedInformation;
			this.Gender = this.pedPersona.Gender.ToString();
			this.MedicalProblems = new List<string>();
		}

		
		public Citizen(Vector3 spawnPoint, float heading) : base(spawnPoint, heading)
		{
			this.pedPersona = Functions.GetPersonaForPed(this);
			this.FullName = this.pedPersona.FullName;
			this.Forename = this.pedPersona.Forename;
			this.TimesStopped = this.pedPersona.TimesStopped.ToString();
			this.Wanted = this.pedPersona.Wanted;
			this.WantedInformation = this.pedPersona.WantedInformation;
			this.Gender = this.pedPersona.Gender.ToString();
			this.MedicalProblems = new List<string>();
		}

		
		public Citizen(Vector3 spawnPoint, Model modelName, float heading) : base(modelName, spawnPoint, heading)
		{
			this.pedPersona = Functions.GetPersonaForPed(this);
			this.FullName = this.pedPersona.FullName;
			this.Forename = this.pedPersona.Forename;
			this.TimesStopped = this.pedPersona.TimesStopped.ToString();
			this.Wanted = this.pedPersona.Wanted;
			this.WantedInformation = this.pedPersona.WantedInformation;
			this.Gender = this.pedPersona.Gender.ToString();
			this.MedicalProblems = new List<string>();
		}

		
		public Citizen(Vector3 spawnPoint) : base(spawnPoint)
		{
			this.pedPersona = Functions.GetPersonaForPed(this);
			this.FullName = this.pedPersona.FullName;
			this.Forename = this.pedPersona.Forename;
			this.TimesStopped = this.pedPersona.TimesStopped.ToString();
			this.Wanted = this.pedPersona.Wanted;
			this.WantedInformation = this.pedPersona.WantedInformation;
			this.Gender = this.pedPersona.Gender.ToString();
			this.MedicalProblems = new List<string>();
		}

		
		public void setMedicalProblemsForEscapedSuspect()
		{
			this.MedicalProblems.Clear();
			this.Wanted = true;
			List<string> CMP = this.commonMedicalProblems;
			for (int i = 0; i < this.monke.Next(1, 3); i++)
			{
				int num = this.monke.Next(0, CMP.Count);
				this.MedicalProblems.Add(CMP[num]);
				CMP.RemoveRange(num, 1);
			}
		}

		
		public void setMedicalProblemsForMentallyIllSuspect()
		{
			this.MedicalProblems.Clear();
			this.Wanted = false;
			List<string> CMHP = this.commonMentalHealthProblems;
			for (int i = 0; i < this.monke.Next(1, 3); i++)
			{
				int num = this.monke.Next(0, CMHP.Count);
				this.MedicalProblems.Add(CMHP[num]);
				CMHP.RemoveRange(num, 1);
			}
		}

		
		public static string ListToString(List<string> list)
		{
			return string.Join(", ", list);
		}

		
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"~w~ Wanted Status: ~y~",
				this.Wanted.ToString(),
				"\n~w~ Times Stopped: ~o~ ",
				this.TimesStopped,
				"\n~w~ Medical History: ~r~",
				Citizen.ListToString(this.MedicalProblems)
			});
		}

		
		private Random monke = new Random();

		
		private List<string> commonMedicalProblems = new List<string>
		{
			"Multiple Lacerations all over upper body and face",
			"Gunshot wounds in the thigh, arm, neck",
			"Pneumothorax",
			"Shattered Femur",
			"Grade 3 Concussion",
			"3rd Degree Burns",
			"Broken Nose",
			"Broken Orbital",
			"Stab wounds in the stomach",
			"Bruise marks on wrists and forearms"
		};

		
		private List<string> commonMentalHealthProblems = new List<string>
		{
			"Depression",
			"Generalised anxiety disorder",
			"Panic Disorder",
			"Obsessive-Compulsive Disorder",
			"Post-Traumatic Stress Disorder",
			"Dissociative Identity Disorder",
			"Paranoid Personality Disorder",
			"Schizophrenia",
			"Social Anxiety Disorder",
			"Nosocomephobia(Fear of hospitals)"
		};

		
		private Persona pedPersona;
	}
}
