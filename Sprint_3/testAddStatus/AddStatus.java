package testAddStatus;

import java.util.List;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import java.time.Duration;

public class AddStatus {
    public static void main(String[] args) throws InterruptedException {
        
        // Khởi tạo trình duyệt
        WebDriver driver = new ChromeDriver();
        driver.manage().window().maximize();
        
        // URL và thông tin đăng nhập
        driver.get("https://localhost:7155/");
        
        // Đợi trang tải
        WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
        
        // Tên tài khoản
        WebElement tk = wait.until(ExpectedConditions.visibilityOfElementLocated(By.xpath("//input[@id='tenDangNhap']")));
        tk.sendKeys("phongaa");
        
        // Mật khẩu
        WebElement mk = driver.findElement(By.xpath("//input[@id='matKhau']"));
        mk.sendKeys("1234");
        
        // Bấm login
        WebElement login = driver.findElement(By.xpath("//button[contains(text(),'Đăng nhập')]"));
        login.click();
        
        // Chọn thông tin cá nhân
        WebElement view = wait.until(ExpectedConditions.elementToBeClickable(By.xpath("//a[normalize-space()='Thông tin cá nhân']")));
        view.click();
        
        // Kiểm tra sự tồn tại của phần tử thông tin cá nhân (ví dụ: tên người dùng hoặc email)
        List<WebElement> userInfo = driver.findElements(By.xpath("//main[@class='main-content']"));
        if (!userInfo.isEmpty()) {
            System.out.println("Thông tin cá nhân đã được hiển thị.");
        } else {
            System.out.println("Không thể tìm thấy thông tin cá nhân.");
        }
        
        // Chọn thông báo
        WebElement tb = driver.findElement(By.xpath("//a[normalize-space()='Thông báo']"));
        tb.click();
        
        // Tạo bài đăng mới
        WebElement createPostButton = driver.findElement(By.xpath("//a[contains(text(),'Tạo mới bài đăng')]"));
        createPostButton.click();
        
        // Thêm nội dung
        WebElement nd = driver.findElement(By.xpath("//input[@id='NoiDung']"));
        nd.sendKeys("ảnh đẹp");
        
        // Thêm ảnh
        WebElement anh = driver.findElement(By.xpath("//input[@id='DuongDanAnh']"));
        anh.sendKeys("https://cdn2.fptshop.com.vn/unsafe/640x0/filters:quality(100)/2023_5_24_638205649868370260_frame-218.png");
        
        // Tạo bài đăng
        WebElement post = driver.findElement(By.xpath("//input[@value='Create']"));
        post.click();
        
        // Kiểm tra xem bài đăng có xuất hiện không (Kiểm tra nội dung "ảnh đẹp")
        List<WebElement> addposts = driver.findElements(By.xpath("//p[contains(text(),'ảnh đẹp')]"));
        if (!addposts.isEmpty()) {
            System.out.println("Bài đăng đã được tạo thành công.");
        } else {
            System.out.println("Không tìm thấy bài đăng.");
        }
        
        // Xóa bài đăng
        WebElement deletebutton = driver.findElement(By.xpath("//div[2]//div[3]//a[1]"));
        deletebutton.click();
        
        // Chọn delete
        WebElement delete = driver.findElement(By.xpath("//input[@value='Delete']"));
        delete.click();
        
        // Kiểm tra bài đăng có còn trên trang hay không (Sử dụng XPath xác định bài đăng "ảnh đẹp")
        List<WebElement> posts = driver.findElements(By.xpath("//p[contains(text(),'ảnh đẹp')]"));
        if (posts.isEmpty()) {
            System.out.println("Bài đăng đã bị xóa thành công.");
        } else {
            System.out.println("Bài đăng chưa bị xóa.");
        }
        
        // Đóng trình duyệt
        driver.quit();
    }
}
