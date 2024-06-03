package com.example.syp.shipwars.semesterproject.Controllers;

import com.example.syp.shipwars.semesterproject.Planet;
import com.example.syp.shipwars.semesterproject.Services.PlanetService;
import com.example.syp.shipwars.semesterproject.Ship;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/planets")
public class PlanetController {

    private final PlanetService planetService;

    @Autowired
    public PlanetController(PlanetService planetService) {
        this.planetService = planetService;
    }

    @GetMapping
    public ResponseEntity<List<Planet>> getAllPlanets() {
        List<Planet> planets = planetService.getAllPlanets();
        return new ResponseEntity<>(planets, HttpStatus.OK);
    }

    @GetMapping("/{id}")
    public ResponseEntity<Planet> getPlanetById(@PathVariable String id) {
        Optional<Planet> planet = planetService.getPlanetById(id);
        return planet.map(value -> new ResponseEntity<>(value, HttpStatus.OK))
                .orElseGet(() -> new ResponseEntity<>(HttpStatus.NOT_FOUND));
    }

    @PostMapping
    public ResponseEntity<Planet> createPlanet(@RequestBody Planet planet) {
        Planet createdPlanet = planetService.savePlanet(planet);
        return new ResponseEntity<>(createdPlanet, HttpStatus.CREATED);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Planet> updatePlanet(@PathVariable String id, @RequestBody Planet updatedPlanet) {
        Planet updated = planetService.updatePlanet(id, updatedPlanet);
        return updated != null
                ? new ResponseEntity<>(updated, HttpStatus.OK)
                : new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    @PutMapping("/visited")
    public void updateCountShip(@RequestBody Planet s) {planetService.updateVisitedCountPlanet(s);}

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deletePlanet(@PathVariable String id) {
        planetService.deletePlanetById(id);
        return new ResponseEntity<>(HttpStatus.NO_CONTENT);
    }
}
