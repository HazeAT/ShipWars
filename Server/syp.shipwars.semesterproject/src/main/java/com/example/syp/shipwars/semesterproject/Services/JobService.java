package com.example.syp.shipwars.semesterproject.Services;


import com.example.syp.shipwars.semesterproject.DTOs.Job_DTO;
import com.example.syp.shipwars.semesterproject.DTOs.Risk_DTO;
import com.example.syp.shipwars.semesterproject.Enums.IllegalJobs;
import com.example.syp.shipwars.semesterproject.Enums.IllegalRisks;
import com.example.syp.shipwars.semesterproject.Enums.LegalJobs;
import com.example.syp.shipwars.semesterproject.Enums.LegalRisks;
import com.example.syp.shipwars.semesterproject.Job;
import com.example.syp.shipwars.semesterproject.Planet;
import com.example.syp.shipwars.semesterproject.Repositories.JobRepository;
import com.example.syp.shipwars.semesterproject.Repositories.PlanetRepository;
import com.example.syp.shipwars.semesterproject.Views;
import com.google.gson.Gson;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.FileReader;
import java.io.IOException;
import java.lang.reflect.Type;
import java.util.List;
import java.util.Objects;
import java.util.Random;

@Service
public class JobService {
    @Autowired
    JobRepository jobRepository;
    @Autowired
    PlanetRepository planetRepository;

    //------------------------------------CREATE A NEW JOB-----------------------------------------//
    public Job createJob() {
        Job job = new Job();
        Random rand = new Random();
        int randInt = rand.nextInt(2);

        if(randInt == 0) { //LegalJobs
            randInt = rand.nextInt(LegalJobs.values().length);
            job.setJob(String.valueOf(LegalJobs.values()[randInt]));
            job.setLegalStatus("Legal");
            randInt = rand.nextInt(LegalRisks.values().length);
            job.setRisk(String.valueOf(LegalRisks.values()[randInt]));
            this.setDescription(job);
            this.setSuccess(job);
        }
        else if(randInt == 1) { //IllegalJobs
            randInt = rand.nextInt(IllegalJobs.values().length);
            job.setJob(String.valueOf(IllegalJobs.values()[randInt]));
            job.setLegalStatus("Illegal");
            randInt = rand.nextInt(IllegalRisks.values().length);
            job.setRisk(String.valueOf(IllegalRisks.values()[randInt]));
            this.setDescription(job);
            this.setSuccess(job);
        }
        //Set Planet
        List<Planet> planetList = planetRepository.findAll();
        randInt = rand.nextInt(planetList.size());
        Planet planet = planetList.get(randInt);
        job.setPlanetId(planetList.get(randInt).getId());

        //Set RealSuccess
        randInt = rand.nextInt(job.getMax_success() - job.getMin_success() + 1) + job.getMin_success();
        job.setReal_success(randInt);

        //Set Salary
        if(randInt == 0) { randInt = 1; }
        float randNum = randInt;
        int baseSal = 10000;
        if(Objects.equals(planet.getDiameter(), "0")) { planet.setDiameter("1");}
        float salary = baseSal / (randNum / 100) + rand.nextInt(Integer.parseInt(planet.getDiameter()));
        job.setSalary((int)salary);
        return jobRepository.save(job);
    }
    private void setDescription(Job job) {
        Gson gson = new Gson();
        try (FileReader reader = new FileReader("C:/Users/Tobias Haas/Desktop/Schule/POS/ShipWars/Server/syp.shipwars.semesterproject/src/main/java/com/example/syp/shipwars/semesterproject/JSONS/job_descr.json")){
            Job_DTO[] jobs = gson.fromJson(reader,(Type) Job_DTO[].class);

            for(Job_DTO j : jobs) {
                if(j.getJob().equals(job.getJob())) {
                    job.setDescription(j.getDescription());
                    return;
                }
            }
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }
    private void setSuccess(Job job) {
        Gson gson = new Gson();
        try (FileReader reader = new FileReader("C:/Users/Tobias Haas/Desktop/Schule/POS/ShipWars/Server/syp.shipwars.semesterproject/src/main/java/com/example/syp/shipwars/semesterproject/JSONS/job_risk.json")){
            Risk_DTO[] risks = gson.fromJson(reader,(Type) Risk_DTO[].class);
            for(Risk_DTO r : risks) {
                if(r.getRisk().equals(job.getRisk())) {
                    job.setMin_success(Integer.parseInt(r.getMin_success()));
                    job.setMax_success(Integer.parseInt(r.getMax_success()));
                    return;
                }
            }
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }


    //----------------------------------------- GET ----------------------------------------------//
    public List<Job> getAll() {return jobRepository.findAll();}
    public boolean check(String id) {
        if(jobRepository.existsById(id)) {
            return true;
        }
        return false;
    }
    public Job getJobById(String id) {
        return jobRepository.findById(id).orElse(null);
    }

    //----------------------------------------- DELETE ---------------------------------------------//
    public boolean  deleteById(String id) { jobRepository.deleteById(id); return true;}
}
