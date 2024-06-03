package com.example.syp.shipwars.semesterproject.Repositories;

import com.example.syp.shipwars.semesterproject.User;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface UserRepository extends MongoRepository<User, String> {
    boolean existsByEmail(String email);

    User findByUsername(String username);
}
