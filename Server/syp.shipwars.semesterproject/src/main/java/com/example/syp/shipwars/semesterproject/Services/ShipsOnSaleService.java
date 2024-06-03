package com.example.syp.shipwars.semesterproject.Services;

import com.example.syp.shipwars.semesterproject.DTOs.Ship_DTO;
import com.example.syp.shipwars.semesterproject.ShipOnSale;
import com.example.syp.shipwars.semesterproject.Repositories.ShipsOnSaleRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class ShipsOnSaleService {

    @Autowired
    ShipsOnSaleRepository shipsOnSaleRepository;
    @Autowired
    UserService userService;

    public void prozessExpiredShips(){
        List<ShipOnSale> shipsOnSale = getAll();
        if(!shipsOnSale.isEmpty()) {
            LocalDateTime currentDateTime = LocalDateTime.now();
            for (ShipOnSale s : shipsOnSale) {
                if (s.getExpired().isBefore(currentDateTime)) {
                    addShipToUser(s);
                    deleteByID(s.getId());
                }
            }
        }
    }


    //----------------------------------------------GET--------------------------------------------------//
    public List<ShipOnSale> getAll(){return shipsOnSaleRepository.findAll();}
    public ShipOnSale getById( String id){return shipsOnSaleRepository.findById(id).orElse(null);}

    //----------------------------------------------POST--------------------------------------------------//
    public ShipOnSale createSale(ShipOnSale sosdto){
        sosdto.setCreated(LocalDateTime.now());
        sosdto.setExpired(LocalDateTime.now().plusDays(3));
        return shipsOnSaleRepository.save(sosdto);
    }

    public void addShipToUser(ShipOnSale s){
        Ship_DTO sdto = new Ship_DTO();
        sdto.setId(s.getShip().getId());
        sdto.setRented("false");
        userService.addOwned(s.getUserID(), sdto);
    }

    //----------------------------------------------DELETE--------------------------------------------------//
    public void deleteByID(String id) {shipsOnSaleRepository.deleteById(id);}
}
