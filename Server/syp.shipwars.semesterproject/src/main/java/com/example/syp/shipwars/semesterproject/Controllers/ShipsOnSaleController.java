package com.example.syp.shipwars.semesterproject.Controllers;

import com.example.syp.shipwars.semesterproject.ShipOnSale;
import com.example.syp.shipwars.semesterproject.Services.ShipsOnSaleService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/sales")
public class ShipsOnSaleController {
    @Autowired
    private ShipsOnSaleService shipsOnSaleService;

    //----------------------------------------------Get - Mapping--------------------------------------------------//
    @GetMapping
    public List<ShipOnSale> getAll() {return shipsOnSaleService.getAll();}
    @GetMapping("/{id}")
    public ShipOnSale getByID(@PathVariable String id) {return shipsOnSaleService.getById(id);}

    //----------------------------------------------Post - Mapping--------------------------------------------------//
    @PostMapping
    public ShipOnSale createSale(@Valid @RequestBody ShipOnSale sosdto) {return shipsOnSaleService.createSale(sosdto);}

    //----------------------------------------------Post - Mapping--------------------------------------------------//
    @DeleteMapping("/{id}")
    public void deleteSale(@PathVariable String id) {shipsOnSaleService.deleteByID(id);}
}
