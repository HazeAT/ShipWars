package com.example.syp.shipwars.semesterproject.Repositories;

import com.example.syp.shipwars.semesterproject.Planet;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PlanetRepository extends MongoRepository<Planet, String> {
    // You can add custom query methods here if needed
}
