package com.example.syp.shipwars.semesterproject.Services;

import com.example.syp.shipwars.semesterproject.Repositories.ShipRepository;
import com.example.syp.shipwars.semesterproject.Ship;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class ShipService {

    private final ShipRepository shipRepository;

    @Autowired
    public ShipService(ShipRepository shipRepository) {
        this.shipRepository = shipRepository;
    }

    // Create
    public Ship saveShip(Ship ship) {
        return shipRepository.save(ship);
    }

    // Read
    public List<Ship> getAllShips() {
        return shipRepository.findAll();
    }

    public Ship getShipById(String id) {
        return shipRepository.findById(id).orElse(null);
    }

    public String getNameByModel(String model) {
        return shipRepository.findNameByModel(model).getName();
    }

    // Update
    public Ship updateShip(String id, Ship updatedShip) {
        if (shipRepository.existsById(id)) {
            updatedShip.setId(id);
            return shipRepository.save(updatedShip);
        }
        return null; // Handle not found scenario
    }
    public void updateSoldCountShip(Ship updateShip){
        if(shipRepository.existsById(updateShip.getId())) {
            Ship s = shipRepository.findById(updateShip.getId()).orElse(null);
            s.setSold(s.getSold()+1);
            shipRepository.save(s);
        }
    }

    // Delete
    public void deleteShipById(String id) {
        shipRepository.deleteById(id);
    }
}
