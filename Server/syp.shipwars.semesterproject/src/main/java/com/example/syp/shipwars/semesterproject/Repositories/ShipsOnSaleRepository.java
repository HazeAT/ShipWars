package com.example.syp.shipwars.semesterproject.Repositories;
import com.example.syp.shipwars.semesterproject.ShipOnSale;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface ShipsOnSaleRepository extends MongoRepository<ShipOnSale, String> {
}
