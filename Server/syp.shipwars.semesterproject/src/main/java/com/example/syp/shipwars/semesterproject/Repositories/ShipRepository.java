package com.example.syp.shipwars.semesterproject.Repositories;

import com.example.syp.shipwars.semesterproject.Ship;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface ShipRepository extends MongoRepository<Ship, String> {
    Ship findNameByModel(String model);
}
