package com.traveling.salesman.problem.main;

import java.util.*;
import java.util.stream.IntStream;

/**
 * This class represents a population. Every population has a set of {@link Individual} which are stored in an array.
 *
 */

public class Population {
    private Individual[] population;

    /**
     *  Every time a {@link Population} object is created, data from a {@link TSPData} object and a specific size of the
     *  population have to be provided.
     * @param size      The quantity of the {@link Individual} objects contained in this class.
     * @param tspData   The data for creating a {@link Population}.
     */
    public Population(int size, TSPData tspData) {
        if(tspData==null){
            System.out.println("Population was not created: No TSPData was created yet!");
        } else{
            this.population = new Individual[size];
            creRandPop(tspData.getDimension(), tspData.getFitMatrix());
        }

    }

    /**
     * Methode to create random {@link Individual} and storing them in the {@link Population} object.
     * @param dimension     The size of the gene int[] for each {@link Individual}
     * @param fitMatrix     The matrix containing all the distances between the {@link City}cities.
     */
    private void creRandPop(int dimension, int[][] fitMatrix) {
        for (int i = 0; i < population.length; i++) {
            int[] gene = IntStream.range(1,dimension+1).toArray();
            Shuffle.shuffle(gene);
            population[i] = new Individual(gene, fitMatrix);
        }
    }

    @Override
    public String toString(){
        StringBuilder retString = new StringBuilder();
        Arrays.stream(population).forEach(e -> retString.append(e.toString()).append("\n"));
        return retString.toString();
    }

    public Individual[] getPopulation() {
        return population;
    }

    public int getSize() {
        return population.length;
    }

    public void setPopulation(Individual[] population) {
        this.population = population;
    }

}
