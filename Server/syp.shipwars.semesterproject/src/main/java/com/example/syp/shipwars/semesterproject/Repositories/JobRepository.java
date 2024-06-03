package com.example.syp.shipwars.semesterproject.Repositories;

import com.example.syp.shipwars.semesterproject.Job;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface JobRepository extends MongoRepository<Job,String> {
}
