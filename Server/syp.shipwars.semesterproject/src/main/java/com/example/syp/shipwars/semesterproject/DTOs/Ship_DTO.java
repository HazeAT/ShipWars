package com.example.syp.shipwars.semesterproject.DTOs;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class Ship_DTO {
    private String id;
    private int amount;
    private String model;
    private String value;
    private int health;
    private int missions;
    private String rented;
}
 