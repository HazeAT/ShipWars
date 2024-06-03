package com.example.syp.shipwars.semesterproject.Controllers;

import com.example.syp.shipwars.semesterproject.Job;
import com.example.syp.shipwars.semesterproject.Repositories.JobRepository;
import com.example.syp.shipwars.semesterproject.Services.JobService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/jobs")
public class JobController {
    @Autowired
    JobService jobService;


    //-------------------------------------------- POST ----------------------------------------------//
    @PostMapping
    public Job createJob() { return jobService.createJob();}

    //-------------------------------------------- GET ------------------------------------------------//
    @GetMapping
    public List<Job> getAllJobs() { return jobService.getAll();}

    @GetMapping("/checkAvailability/{id}")
    public boolean checkAvailability(@PathVariable String id) { return jobService.check(id);}

    @GetMapping("/accept/{id}")
    public Job acceptJob(@PathVariable String id) {
        Job job = jobService.getJobById(id);
        jobService.deleteById(id);
        jobService.createJob();
        return job;
    }

    @DeleteMapping("/{id}")
    public boolean deleteById(@PathVariable String id){ return jobService.deleteById(id);}
}
