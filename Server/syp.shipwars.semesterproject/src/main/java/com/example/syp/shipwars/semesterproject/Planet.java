package com.example.syp.shipwars.semesterproject;

import jakarta.persistence.Id;
import lombok.Getter;
import lombok.Setter;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "planets")
@Getter
@Setter
public class Planet {
    @Id private String id;
    private String diameter;
    private String rotation_period;
    private String orbital_period;
    private String gravity;
    private String population;
    private String climate;
    private String terrain;
    private String surface_water;
    private String name;
    private int visited = 0;
}
