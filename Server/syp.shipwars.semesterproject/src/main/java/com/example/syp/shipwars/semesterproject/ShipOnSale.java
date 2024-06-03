package com.example.syp.shipwars.semesterproject;

import com.example.syp.shipwars.semesterproject.DTOs.Ship_DTO;
import com.fasterxml.jackson.annotation.JsonView;
import jakarta.persistence.Id;
import lombok.Getter;
import lombok.Setter;
import org.springframework.data.mongodb.core.mapping.Document;

import java.time.LocalDateTime;

@Document(collection = "sale")
@Getter @Setter
public class ShipOnSale {
    @JsonView(Views.Public.class)
    @Id String id;
    @JsonView(Views.Public.class)
    private Ship_DTO ship;
    @JsonView(Views.Public.class)
    private String userID;
    @JsonView(Views.Public.class)
    private String price;
    @JsonView(Views.Public.class)
    private int health;
    @JsonView(Views.Public.class)
    private LocalDateTime created;
    @JsonView(Views.Public.class)
    private LocalDateTime expired;
}
