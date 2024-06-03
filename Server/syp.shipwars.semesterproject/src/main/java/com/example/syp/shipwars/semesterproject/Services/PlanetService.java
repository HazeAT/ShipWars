package com.example.syp.shipwars.semesterproject.Services;

import com.example.syp.shipwars.semesterproject.Planet;
import com.example.syp.shipwars.semesterproject.Repositories.PlanetRepository;
import com.example.syp.shipwars.semesterproject.Ship;
import org.apache.logging.log4j.util.PropertySource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Comparator;
import java.util.List;
import java.util.Optional;

@Service
public class PlanetService {

    private final PlanetRepository planetRepository;

    @Autowired
    public PlanetService(PlanetRepository planetRepository) {
        this.planetRepository = planetRepository;
    }

    public List<Planet> getAllPlanets() {

        List<Planet> p = planetRepository.findAll();
        p.sort(Comparator.comparingDouble((Planet planet) -> Double.parseDouble(planet.getPopulation())).reversed());
        return p;
    }

    public Optional<Planet> getPlanetById(String id) {
        return planetRepository.findById(id);
    }

    public Planet savePlanet(Planet planet) {
        return planetRepository.save(planet);
    }

    public Planet updatePlanet(String id, Planet updatedPlanet) {
        if (planetRepository.existsById(id)) {
            updatedPlanet.setId(id);
            return planetRepository.save(updatedPlanet);
        }
        return null;
    }

    public void updateVisitedCountPlanet(Planet planet  ){
        if(planetRepository.existsById(planet.getId())) {
            Planet p = planetRepository.findById(planet.getId()).orElse(null);
            p.setVisited(p.getVisited()+1);
            planetRepository.save(p);
        }
    }
    public void deletePlanetById(String id) {
        planetRepository.deleteById(id);
    }
}
