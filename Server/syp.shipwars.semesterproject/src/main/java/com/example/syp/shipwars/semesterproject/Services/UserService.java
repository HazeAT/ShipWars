package com.example.syp.shipwars.semesterproject.Services;

import com.example.syp.shipwars.semesterproject.DTOs.Ship_DTO;
import com.example.syp.shipwars.semesterproject.DTOs.UserID_DTO;
import com.example.syp.shipwars.semesterproject.Enums.Roles;
import com.example.syp.shipwars.semesterproject.Repositories.UserRepository;
import com.example.syp.shipwars.semesterproject.Ship;
import com.example.syp.shipwars.semesterproject.ShipOnSale;
import com.example.syp.shipwars.semesterproject.User;
import jakarta.el.CompositeELResolver;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Lazy;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Collections;
import java.util.Comparator;
import java.util.List;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;
    @Autowired
    private ShipService shipService;
    @Autowired @Lazy
    private ShipsOnSaleService shipsOnSaleService;
    @Autowired
    private PasswordEncoder passwordEncoder;
    //----------------------------------------------GET--------------------------------------------------//
    public List<User> getAllUsers() {
        List<User> users = userRepository.findAll();
        users.sort(Comparator.comparingLong(User::getCredits).reversed());
        return users;
    }
    public User getUserById(String id) {
        return userRepository.findById(id).orElse(null);
    }
    public User getFriendById(String id, UserID_DTO udto) {
        if(userRepository.existsById(id)) {
            User user = getUserById(id);
            for(UserID_DTO u : user.getFriends()) {
                if(u.getId().equals(udto.getId())) {
                    return getUserById(udto.getId());
                }
            }
        }
        return null;
    }

    public User getUserByUsername(String name) {
        for(User u : getAllUsers()) {
            if(u.getUsername().equals(name)) {
                return u;
            }
        }
        return null;
    }

    public List<Ship_DTO> getOwnedShipsById(String id) {
        if(userRepository.existsById(id)) {
            return userRepository.findById(id).get().getOwnedships();
        }
        return null;
    }
    public String getIdByUsername(String username) {
        User user = userRepository.findByUsername(username);
        return user.getId();
    }
    public User getUserByEmail(String email) {
        for(User u : getAllUsers()) {
            if(u.getEmail().toUpperCase().equals(email.toUpperCase())) {
                return u;
                }
        }
        return null;
    }
    public boolean checkPassword(String email, String password) {
        if(userRepository.existsByEmail(email)){
            User u = getUserByEmail(email);
            return passwordEncoder.matches(password, u.getPassword());
        }
        return false;
    }

    //----------------------------------------------POST--------------------------------------------------//
    public User createUser(User user) {
        user.setPassword(passwordEncoder.encode(user.getPassword()));
        user.setCredits(50000);
        user.setRole(Roles.ENSIGNED);
        return userRepository.save(user);
    }


    //----------------------------------------------Put--------------------------------------------------//
    public ResponseEntity<String> addOwned (String id, Ship_DTO shipid) {
        if (userRepository.existsById(id)) {
            ResponseEntity<String> s = null;
            User user = getUserById(id);
            if (shipService.getShipById(shipid.getId()) != null) {
                Ship ship = shipService.getShipById(shipid.getId());
                s = user.addOwned(ship, null, shipid.getRented());
            } else if (shipsOnSaleService.getById(shipid.getId()) != null) {
                ShipOnSale sos = shipsOnSaleService.getById(shipid.getId());
                s = user.addOwned(shipService.getShipById(sos.getShip().getId()), sos, shipid.getRented());
            }
            userRepository.save(user);
            return s;
        }
        return null;
    }
    public ResponseEntity<String>  addFriend(String id, String uid) {
        if(userRepository.existsById(id)) {
            User user = getUserById(id);
            if(userRepository.existsById(uid)){
                ResponseEntity<String> r = user.addFriend(getUserById(uid));
                userRepository.save(user);
                return r;
            }
        }
        return null;
    }
    public ResponseEntity<String> updateCreditsUser(String id, User user) {
        if (userRepository.existsById(id)) {
            long money = user.getCredits();
            user = getUserById(id);
            user.setCredits(money);
            userRepository.save(user);
            return ResponseEntity.ok("True");
        }
        return ResponseEntity.ok("False");
    }
    public void updateLevel(User user) {
        User u = userRepository.findByUsername(user.getUsername());
        if(u != null) {
            u.setLevel(u.getLevel()+1);
            checkRank(u);
            userRepository.save(u);
        }
    }
    private void checkRank(User u) {
        switch (u.getLevel()) {
            case 10:
                u.setRole(Roles.JUNIOR_LIEUTENANT);
                break;
            case 20:
                u.setRole(Roles.LIEUTANANT);
                break;
            case 30:
                u.setRole(Roles.LIEUTANANT_COMMANDER);
                break;
            case 45:
                u.setRole(Roles.COMMANDER);
                break;
            case 60:
                u.setRole(Roles.VICE_CAPTAIN);
                break;
            case 80:
                u.setRole(Roles.CAPTAIN);
                break;
            case 105:
                u.setRole(Roles.REAR_ADMIRAL);
                break;
            case 135:
                u.setRole(Roles.VICE_ADMIRAL);
                break;
            case 165:
                u.setRole(Roles.ADMIRAL);
                break;
            case 200:
                u.setRole(Roles.FLEET_ADMIRAL);
                break;
            case 350:
                u.setRole(Roles.GRAND_ADMIRAL);
                break;
        }
    }

    public void editUsedShipHealth(String username, Ship_DTO ss) {
        User u = userRepository.findByUsername(username);
        Ship_DTO s = null;
        if(u != null) {
            for (Ship_DTO sdto : u.getOwnedships()) {
                if (sdto.getId().equals(ss.getId())) {
                    s = sdto;
                    break;
                }
            }
            if(s != null) {
                u.getOwnedships().remove(s);
                s.setHealth(s.getHealth()-1);
                s.setValue(String.valueOf((Long.parseLong(s.getValue())/100)*s.getHealth()));
                u.getOwnedships().add(s);
            }
        }
        userRepository.save(u);
    }


    //----------------------------------------------Delete--------------------------------------------------//
    public void deleteUser(String id) {
        userRepository.deleteById(id);
    }
    public ResponseEntity<String> deleteFriend(String id, String uid) {
        if(userRepository.existsById(id)) {
            User user = getUserById(id);
            if(user.getFriends()!=null) {
                ResponseEntity<String> r = user.deleteFriend(uid);
                userRepository.save(user);
                return r;
            }
        }
        return null;
    }
    public boolean deleteOwned(String uid, String sid) {
        if (userRepository.existsById(uid)) {
            User user = getUserById(uid);

            for (Ship_DTO s : user.getOwnedships()) {
                if (s.getId().equals(sid)) {
                    if (s.getAmount() > 1) {
                        s.setAmount(s.getAmount() - 1);
                        userRepository.save(user);
                        return true;
                    }
                    user.getOwnedships().remove(s);
                    userRepository.save(user);
                    return true;
                }
            }
        }
        return false;
    }
}
