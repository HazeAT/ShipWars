package com.example.syp.shipwars.semesterproject.Controllers;

import com.example.syp.shipwars.semesterproject.Services.ShipService;
import com.example.syp.shipwars.semesterproject.Ship;
import jakarta.persistence.Convert;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Comparator;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/ships")
public class ShipController {

    private final ShipService shipService;

    @Autowired
    public ShipController(ShipService shipService) {
        this.shipService = shipService;
    }

    @GetMapping
    public ResponseEntity<List<Ship>> getAllShips() {
        List<Ship> ships = shipService.getAllShips();
        ships.sort(Comparator.comparingDouble((Ship ship) -> Double.parseDouble(ship.getCost_in_credits())).reversed());
        return new ResponseEntity<>(ships, HttpStatus.OK);
    }

    @GetMapping("/{id}")
    public Ship getShipById(@PathVariable String id) {
        return shipService.getShipById(id);
    }

    @GetMapping("/nameof/{model}")
    public String getNameByModel(@PathVariable String model) { return shipService.getNameByModel(model); }

    @PostMapping
    public ResponseEntity<Ship> createShip(@RequestBody Ship ship) {
        Ship createdShip = shipService.saveShip(ship);
        return new ResponseEntity<>(createdShip, HttpStatus.CREATED);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Ship> updateShip(@PathVariable String id, @RequestBody Ship updatedShip) {
        Ship updated = shipService.updateShip(id, updatedShip);
        return updated != null
                ? new ResponseEntity<>(updated, HttpStatus.OK)
                : new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    @PutMapping("/count")
    public void updateCountShip(@RequestBody Ship s) {shipService.updateSoldCountShip(s);}


    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteShip(@PathVariable String id) {
        shipService.deleteShipById(id);
        return new ResponseEntity<>(HttpStatus.NO_CONTENT);
    }
}
