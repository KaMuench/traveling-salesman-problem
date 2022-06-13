package com.traveling.salesman.problem.gui;

import javax.swing.*;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import java.awt.event.FocusEvent;
import java.awt.event.FocusListener;
import java.util.concurrent.atomic.AtomicBoolean;

public class Listener {
    public record TextValueListener(JSlider slider, JTextField txtField,
                                    AtomicBoolean readyToRead) implements DocumentListener {

        @Override
        public void insertUpdate(DocumentEvent e) {
            common(e);
        }

        @Override
        public void removeUpdate(DocumentEvent e) {
            common(e);
        }

        @Override
        public void changedUpdate(DocumentEvent e) {
        }

        private void common(DocumentEvent e) {
            if (readyToRead.compareAndExchange(true, false)) {
                try {
                    int num = Integer.parseInt(txtField.getText());
                    slider.setValue(num);
                    readyToRead.set(true);
                } catch (NumberFormatException exception) {

                    readyToRead.set(true);
                }
            }
        }
    }

    public record SliderListener(JSlider slider, JTextField txtField,
                                 AtomicBoolean readyToRead) implements ChangeListener {

        @Override
        public void stateChanged(ChangeEvent e) {
            if (readyToRead.compareAndExchange(true, false)) {
                txtField.setText(String.valueOf(slider.getValue()));
                readyToRead.set(true);
            }
        }
    }

    public record TextFocusListener(JSlider slider,JTextField textField ) implements FocusListener {

        @Override
        public void focusGained(FocusEvent e) {

        }

        @Override
        public void focusLost(FocusEvent e) {
            try {
                Integer.parseInt(textField.getText());
            } catch (NumberFormatException ne){
                textField.setText(String.valueOf(slider.getValue()));
            }
        }
    }

}
