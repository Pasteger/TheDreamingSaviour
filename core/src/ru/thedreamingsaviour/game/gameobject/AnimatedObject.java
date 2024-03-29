package ru.thedreamingsaviour.game.gameobject;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Sprite;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;

import java.util.ArrayList;
import java.util.List;

import static ru.thedreamingsaviour.game.resourceloader.TextureLoader.getNullTexture;

public class AnimatedObject {
    private List<Texture> textures = new ArrayList<>();
    private final Sprite sprite = new Sprite(getNullTexture());
    private int currentFrame;
    private int speed;

    public AnimatedObject(List<Texture> textures) {
        this.textures = textures;
    }
    public AnimatedObject() {}

    public void draw(SpriteBatch batch, float x, float y, float width, float height, int speed){
        if (currentFrame > textures.size() - 1) {
            currentFrame = 0;
        }
        sprite.setTexture(textures.get(currentFrame));
        sprite.setBounds(x, y, width, height);
        sprite.draw(batch);
        if (this.speed == speed) {
            currentFrame++;
            this.speed = 0;
        } else {
            this.speed++;
        }
    }

    public void setTextures(List<Texture> textures) {
        if (this.textures.size() != textures.size()) {
            speed = 0;
        }
        this.textures = textures;
    }
    public void rotateSprite(float degree) {
        if (degree != sprite.getRotation()) {
            sprite.setRotation(degree);
        }
    }
    public void mirrorSprite(boolean x, boolean y){
        sprite.setFlip(x, y);
    }
    public List<Texture> getTextures() {
        return textures;
    }
    public void setSpriteColor(float r, float g, float b, float a){
        sprite.setColor(r, g ,b, a);
    }
}
