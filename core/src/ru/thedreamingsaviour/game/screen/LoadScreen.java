package ru.thedreamingsaviour.game.screen;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import ru.thedreamingsaviour.game.MyGdxGame;
import ru.thedreamingsaviour.game.resourceloader.*;

import java.util.Arrays;

public class LoadScreen implements Screen {
    private final MyGdxGame game;
    private final OrthographicCamera camera;
    private String message;
    private String error;
    private boolean success;

    public LoadScreen(final MyGdxGame gam) {
        message = "Loading...";
        this.game = gam;
        camera = new OrthographicCamera();
        camera.setToOrtho(false, 3000, 2500);
        Gdx.gl.glClearColor(0.2f, 0, 0.2f, 1);

        new Thread(() -> Gdx.app.postRunnable(() -> {
            try {
                message = "Loading textures";
                error = "Error from textures";
                TextureLoader.load();
                message = "Loading sounds";
                error = "Error from sounds";
                SoundLoader.load();
                message = "Loading music";
                error = "Error from music";
                MusicLoader.load();
                message = "Loading dialogues";
                error = "Error from dialogues";
                DialogueLoader.load();
                message = "Loading success";
                success = true;
            } catch (Exception exception) {
                exception.printStackTrace();
                message = error + "\n" + Arrays.toString(exception.getStackTrace());
            }
        })).start();
    }

    @Override
    public void render(float delta) {
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

        camera.update();
        game.batch.setProjectionMatrix(camera.combined);

        game.batch.begin();
        game.universalFont.draw(game.batch, message, 80, 210);
        game.batch.end();
        if (success) {
            game.setScreen(new MainMenuScreen(game));
        }
    }

    @Override public void show(){}
    @Override public void resize(int width, int height){}
    @Override public void pause(){}
    @Override public void resume(){}
    @Override public void hide(){}
    @Override public void dispose(){}
}
