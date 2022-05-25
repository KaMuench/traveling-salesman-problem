package com.traveling.salesman.problem.main;

/**
 * This class {@link City} is used to represent a city object which has coordinates and an id.
 */
public class City {
    private final int x;
    private final int y;
    private final int id;

    public City(int id, int x, int y ) {
        this.x=x;
        this.y=y;
        this.id=id;
    }

    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }

    public int getId() {
        return id;
    }
}
