package com.example.syp.shipwars.semesterproject;

import com.example.syp.shipwars.semesterproject.DTOs.Ship_DTO;
import com.example.syp.shipwars.semesterproject.DTOs.UserID_DTO;
import com.example.syp.shipwars.semesterproject.Enums.Roles;
import com.fasterxml.jackson.annotation.JsonView;
import jakarta.validation.constraints.Email;
import lombok.Getter;
import lombok.NonNull;
import lombok.Setter;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.http.ResponseEntity;

import java.util.ArrayList;
import java.util.List;

@Document(collection = "users")
@Getter @Setter
public class User {
    @Id @NonNull
    @JsonView(Views.Internal.class)
    private String id;
    @NonNull
    @JsonView(Views.Public.class)
    private String username;
    @JsonView(Views.Public.class)
    @NonNull
    private int level;
    @JsonView(Views.Public.class)
    private Roles role = Roles.ENSIGNED;
    @Email @NonNull
    @JsonView(Views.ExtendedPublic.class)
    private String email;
    @NonNull
    @JsonView(Views.ExtendedPublic.class)
    private String password;
    @JsonView(Views.ExtendedPublic.class)
    private long credits;
    @JsonView(Views.ExtendedPublic.class)
    private List<UserID_DTO> friends;
    @JsonView(Views.ExtendedPublic.class)
    private List<Ship_DTO> ownedships;
    //---------------------------FriendsList - Add - Remove  -----------------------------//
    public ResponseEntity<String> addFriend(User user) {
        if(this.friends == null) {
            friends = new ArrayList<>();
        } else {
            for(UserID_DTO u : friends) {
                if(u.getId().equals(user.getId())) {
                    return ResponseEntity.ok("False");
                }
            }
        }
        UserID_DTO uidto = new UserID_DTO();
        uidto.setId(user.id);
        this.friends.add(uidto);
        System.out.println(friends.size());
        return ResponseEntity.ok("True");
    }
    public ResponseEntity<String> deleteFriend(String id) {
        UserID_DTO remove = null;
        for(UserID_DTO u : friends) {
            if(u.getId().equals(id)) {
                remove = u;
                break;
            }
        }
        if(remove != null) {
            friends.remove(remove);
            return ResponseEntity.ok("True");
        }
        return ResponseEntity.ok("False");
    }

    //---------------------------Ownedships - Add- Remove---------------------//
    public ResponseEntity<String> addOwned(Ship ship, ShipOnSale sos, String rented) {
        Ship_DTO uidto = new Ship_DTO();

        if(ship != null) {
            uidto.setHealth(99);
            if(sos != null) {
                uidto.setHealth(sos.getHealth());
            }
            uidto.setId(ship.getId());
            if(rented.equals("True")) {
                uidto.setValue("0");
            } else {
                uidto.setValue(String.valueOf((Long.parseLong(ship.getCost_in_credits()) / 100 * uidto.getHealth())));
            }
            uidto.setModel(ship.getModel());
        }
        if(this.ownedships == null) {
            ownedships = new ArrayList<>();
        } else {
            for (Ship_DTO s : ownedships) {
                if(s.getId().equals(uidto.getId()) && s.getValue().equals(uidto.getValue()) && s.getHealth() == uidto.getHealth()) {
                    s.setAmount(s.getAmount()+1);
                    return ResponseEntity.ok("True");
                }
            }
        }
        uidto.setAmount(1);
        this.ownedships.add(uidto);
        return ResponseEntity.ok("True");
    }
}
