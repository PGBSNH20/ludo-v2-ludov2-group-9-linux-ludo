// Gulp task for producing output css file in wwwroot/css folder

const gulp = require('gulp');

gulp.task('css', () => {
    const postcss = require('gulp-postcss');

    return gulp.src('./Styles/site.css')
        .pipe(postcss([
            require('precss'),
            require('tailwindcss'),
            require('autoprefixer')
        ]))
        .pipe(gulp.dest('./wwwroot/css/'));
});