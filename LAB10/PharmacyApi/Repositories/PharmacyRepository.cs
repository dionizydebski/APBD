using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Context;
using PharmacyApi.DTOs;
using PharmacyApi.Models;

namespace PharmacyApi.Repositories;

public class PharmacyRepository : IPharmacyRepository
{
    
    public async Task<bool> DoesPatientExist(int idPatient)
    {
        bool result;
            
        await using (var context = new ApbdContext())
        {
            result = await context.Patients.AnyAsync(x => x.IdPatient == idPatient);
        }
        
        return result;
    }

    public async Task<Errors> AddPatient(PatientDTO patientDto)
    {
        await using (var context = new ApbdContext())
        {
            var newPatient = new Patient
            {
                IdPatient = patientDto.IdPatient,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Birthdate = patientDto.BirthDate
            };

            await context.Patients.AddAsync(newPatient);
            await context.SaveChangesAsync();
        }

        return Errors.Good;
    }

    public async Task<bool> DoesMedicamentExist(int idMedicament)
    {
        bool result;
            
        await using (var context = new ApbdContext())
        {
            result = await context.Medicaments.AnyAsync(x => x.IdMedicament == idMedicament);
        }
        
        return result;
    }

    public async Task<int> AddPrescription(PrescriptionInDTO prescriptionInDto)
    {
        int idPrescription;
        
        await using (var context = new ApbdContext())
        {
            var newPrescription = new Prescription
            {
                Date = prescriptionInDto.Date,
                DueDate = prescriptionInDto.DueDate,
                IdPatient = prescriptionInDto.patient.IdPatient,
                IdDoctor = prescriptionInDto.IdDoctor
            };

            await context.Prescriptions.AddAsync(newPrescription);
            await context.SaveChangesAsync();

            idPrescription = newPrescription.IdPrescription;
        }

        return idPrescription;
    }

    public async Task<Errors> AddPrescriptionMedicament(PrescriptionMedicamentDTO prescriptionMedicamentDto)
    {
        await using (var context = new ApbdContext())
        {
            var newPrescriptionMedicament = new Prescription_Medicament
            {
                IdMedicament = prescriptionMedicamentDto.IdMedicament,
                IdPrescription = prescriptionMedicamentDto.IdPrescription,
                Dose = prescriptionMedicamentDto.Dose,
                Details = prescriptionMedicamentDto.Details
            };

            await context.PrescriptionMedicaments.AddAsync(newPrescriptionMedicament);
            await context.SaveChangesAsync();
        }

        return Errors.Good;
    }

    public async Task<PatientDataDTO> GetPatientData(int IdPatient)
    {
        PatientDataDTO patientDataDto = null;
        
        await using (var context = new ApbdContext())
        {

            var patient = await context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(p => p.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicaments)
                .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctors)
                .FirstOrDefaultAsync(p => p.IdPatient == IdPatient);

            patientDataDto = new PatientDataDTO
            (
                Patient: new PatientDTO(patient.IdPatient, patient.FirstName, patient.LastName, patient.Birthdate),
                Prescriptions: patient.Prescriptions
                    .OrderBy(p => p.DueDate)
                    .Select(p => new PrescriptionOutDTO
                    (
                        IdPrescription: p.IdPrescription,
                        Date: p.Date,
                        DueDate: p.DueDate,
                        Medicaments: p.PrescriptionMedicaments.Select(pm => new MedicamentDTO
                        (
                            IdMedicament: pm.Medicaments.IdMedicament,
                            Dose: pm.Dose,
                            Description: pm.Medicaments.Description 
                        )).ToList(),
                        Doctor: new DoctorDTO
                        (
                            p.Doctors.IdDoctor,
                            p.Doctors.FirstName
                            )
                    )).ToList()
            );

        }

        return patientDataDto;
    }
}