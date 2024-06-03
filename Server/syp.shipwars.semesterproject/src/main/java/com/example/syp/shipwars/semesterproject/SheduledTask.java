package com.example.syp.shipwars.semesterproject;

import com.example.syp.shipwars.semesterproject.Repositories.ShipsOnSaleRepository;
import com.example.syp.shipwars.semesterproject.Services.ShipsOnSaleService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
public class SheduledTask {
    @Autowired
    private ShipsOnSaleService shipsOnSaleService;

    @Scheduled(fixedRate = 30000)
    public void checkExpiredDate() {
        shipsOnSaleService.prozessExpiredShips();
    }
}
