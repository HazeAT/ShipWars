package com.example.syp.shipwars.semesterproject;

import jakarta.persistence.Id;
import lombok.Getter;
import lombok.Setter;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "jobs")
@Getter @Setter
public class Job {
    @Id private String id;
    private String job;
    private String planetId;
    private String legalStatus;
    private String risk;
    private int min_success;
    private int max_success;
    private int real_success;
    private String description;
    private int salary;
}
