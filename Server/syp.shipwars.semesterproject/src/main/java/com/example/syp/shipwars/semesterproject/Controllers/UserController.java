package com.example.syp.shipwars.semesterproject.Controllers;

import com.example.syp.shipwars.semesterproject.DTOs.Ship_DTO;
import com.example.syp.shipwars.semesterproject.DTOs.UserID_DTO;
import com.example.syp.shipwars.semesterproject.User;
import com.example.syp.shipwars.semesterproject.Services.UserService;
import com.example.syp.shipwars.semesterproject.Views;
import com.fasterxml.jackson.annotation.JsonView;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/users")
public class UserController {

    @Autowired
    private UserService userService;

    //----------------------------------------------Get - Mapping--------------------------------------------------//
    @GetMapping @JsonView(Views.ExtendedPublic.class)
    public List<User> getAllUsers() {return userService.getAllUsers();}
    @GetMapping("/id/{id}") @JsonView(Views.ExtendedPublic.class)
    public User getUserById(@PathVariable String id) {
        return userService.getUserById(id);
    }
    @GetMapping("/name/{uname}") @JsonView(Views.Internal.class)
    public User getUserByName(@PathVariable String uname) {return userService.getUserByUsername(uname);}
    @GetMapping("/email/{email}") @JsonView(Views.ExtendedPublic.class)
    public User getUserByEmail(@PathVariable String email) {return userService.getUserByEmail(email);}
    @GetMapping("/unametoid/{uname}")
    public String getIDByUsername(@PathVariable String uname) { return userService.getIdByUsername(uname);}
    @GetMapping("/password/{email}/{password}") @JsonView(Views.ExtendedPublic.class)
    public boolean checkPassword(@PathVariable String email, @PathVariable String password) {return userService.checkPassword(email,password);}
    @GetMapping("/friend/{id}") @JsonView(Views.Public.class)
    public User getFriendById(@PathVariable String id, @RequestBody UserID_DTO uid) {return userService.getFriendById(id, uid);}
    @GetMapping("/owned/get/{id}")
    public List<Ship_DTO> getOwnedShips(@PathVariable String id) { return userService.getOwnedShipsById(id);}

    //----------------------------------------------Post - Mapping--------------------------------------------------//
    @PostMapping
    public User createUser( @Valid @RequestBody User user) {
        return userService.createUser(user);
    }

    //----------------------------------------------Put - Mapping--------------------------------------------------//
    @PutMapping("/addowned/{id}") @JsonView(Views.ExtendedPublic.class)
    public ResponseEntity<String> addOwnedShip(@PathVariable String id, @RequestBody Ship_DTO sdto) { return userService.addOwned(id, sdto);}
    @PutMapping("/friend/{id}") @JsonView(Views.ExtendedPublic.class)
    public ResponseEntity<String>  addFriend(@PathVariable String id, @RequestBody UserID_DTO uid) {return userService.addFriend(id,uid.getId());}
    @PutMapping("/{id}") @JsonView(Views.ExtendedPublic.class)
    public ResponseEntity<String>  updateUser(@PathVariable String id, @RequestBody User user) {return userService.updateCreditsUser(id, user);}
    @PutMapping("/update/level")
    public void updateUserLevel(@RequestBody User u) { userService.updateLevel(u);}
    @PutMapping("/update/ship/health/{username}")
    public void updateShipHealth(@PathVariable String username, @RequestBody Ship_DTO s) {userService.editUsedShipHealth(username, s);}

    //----------------------------------------------Delete - Mapping--------------------------------------------------//
    @DeleteMapping("/{id}") @JsonView(Views.ExtendedPublic.class)
    public void deleteUser(@PathVariable String id) {
        userService.deleteUser(id);
    }
    @DeleteMapping("/friend/{id}") @JsonView(Views.ExtendedPublic.class)
    public void deleteFriend(@PathVariable String id, @RequestBody UserID_DTO uid) {userService.deleteFriend(id, uid.getId());}

    @DeleteMapping("/owned/remove/{uid}/{sid}")
    public boolean removeOwnedShip(@PathVariable String uid, @PathVariable String sid) { return userService.deleteOwned(uid, sid);}
}
