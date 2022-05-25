package com.traveling.salesman.problem.main;


import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Stream;

public class TSPData {
    private String name;
    private int dimension;
    private String comment;

    private List<City> cities;
    private int[][] fitMatrix;

    public TSPData(String path) throws IOException{
        getData(path);
        createFitMatrix();
    }

    /**
     *  Methode to read a file containing data for creating an {@link Individual}.
     * @param file                  The path to the *.tsp file
     * @return {@link Individual}   The individual created out of the read data.
     */

    public  Individual readIndividual(String file) {
        try (FileReader fis = new FileReader(file); BufferedReader bis = new BufferedReader(fis)){
            List<Integer> individual = new ArrayList<>();
            String line;
            while((line = bis.readLine())!=null){
                try {
                    int[] array = Stream.of(line.split(" ")).mapToInt(Integer::parseInt).toArray();
                    individual.add(array[0]);
                } catch (NumberFormatException nfexc){
                    //
                }
            }
            individual.set(individual.size()-1, 1);
            int[] array = new int[individual.size()];
            for (int i=0;i<array.length;i++){
                array[i]=individual.get(i);
            }
            return new Individual(array, fitMatrix);

        } catch (FileNotFoundException e){
            System.out.println("Individual was not created: No file found");
        }catch (IOException e){
            System.out.println("Individual was not created: Unknown reason");
        }
        return null;
    }

    /**
     * Is called in the constructor.
     * Reads *.tsp files to create multiple {@link  City} classes and stores them in an {@link ArrayList}.
     *
     * @param file              The path to the *.tsp file
     * @throws IOException      //
     */
    private void getData(String file) throws IOException{
        try (FileReader fis = new FileReader(file); BufferedReader bis = new BufferedReader(fis)) {
            List<City> cities = new ArrayList<>();

            String line;
            while ((line = bis.readLine()) != null) {
                try {
                    //Collecting the coordinates
                    int[] array = Stream.of(line.split(" ")).mapToInt(Integer::parseInt).toArray();
                    City c = new City(array[0], array[1], array[2]);
                    cities.add(c);
                } catch (NumberFormatException nfexc){
                    //Collecting the data for the name, dimension and comments of the TSP.
                    if(line.contains("NAME")){
                        this.name = line.substring(7);
                    } else if(line.contains("DIMENSION")){
                        this.dimension = Integer.parseInt(line.substring(12));
                    } else if(line.contains("COMMENT")){
                        this.comment = line.substring(10);
                    }
                }
            }
            this.cities = cities;
        }
    }

    /**
     * Is called in the constructor.
     * Creating a fitness matrix in which all the distances between the cities are stored.
     */
    private void createFitMatrix(){
        if(cities.size()==0){
            System.out.println("Fitness Matrix was not created: No data to retrieve");
        }else{
            int[][] retArray = new int[cities.size()][cities.size()];
            for(int i=0; i < retArray.length;i++){
                for(int j = 0; j < retArray.length; j++){
                    retArray[i][j] = calcDist(cities.get(i),cities.get(j));
                }
            }
            fitMatrix = retArray;
        }
    }

    /**
     * Using pythagoras to calculate the distance between two cities.
     * @param a     City a
     * @param b     City b
     * @return      Returns the distance between the {@link City} a and the City b.
     */
    public int calcDist(City a, City b){
        return (int) Math.sqrt(Math.pow(a.getX()-b.getX(),2) + Math.pow(a.getY()-b.getY(),2));
    }


    public String getName() {
        return name;
    }

    public int getDimension() {
        return dimension;
    }

    public String getComment() {
        return comment;
    }

    public List<City> getCities() {
        return cities;
    }

    public int[][] getFitMatrix() {
        return fitMatrix;
    }
}
